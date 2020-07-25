using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Sharpshooter.Models;
using Sharpshooter.ViewModel;
using PayPal.Api;

namespace Sharpshooter.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        ApplicationDbContext db = new ApplicationDbContext();

        private string strCart = "Cart";

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult ViewCart()
        {
            return PartialView("Index");
        }

        public ActionResult GeneratePDF()
        {
            var report = new Rotativa.ActionAsPdf("ViewCart");
            return report;
        }
        public ActionResult AddToCart(int id)
        {
            var addedItem = db.MenuItems
                .Single(item => item.MenuItemID == id);

            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedItem);



            return RedirectToAction("Index");
        }

  

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string itemName = db.Carts
                .Single(item => item.RecordId == id).MenuItem.MenuItemTitle;

            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) +
                " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }



        // Work with PayPal Payment
        private Payment payment;

        // Create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };
            if (Session["Cart"] != "")
            {
                List<Cart> listCarts = (List<Cart>)Session[strCart];
                foreach (var cart in listCarts)
                {
                    listItems.items.Add(new Item()
                    {
                        name = cart.MenuItem.MenuItemTitle.ToString(),
                        currency = "USD",
                        price = cart.MenuItem.MenuItemCost.ToString(),
                        quantity = cart.MenuItem.MenuItemQuantity.ToString(), /*cart.RecordId.ToString(),*/
                        sku = "sku"
                    });
                }

            }
            var payer = new Payer() { payment_method = "paypal" };

            // Do the config RedirectURLs here with redirectURLs object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            // Creta details object
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = "1" /*listCarts.Sum(x => x.RecordId * x.MenuItem.MenuItemCost).ToString()*/
            };

            // Create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = Session["SesTotal"].ToString(), /*(Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),*/
                details = details
            };

            // Create transaction
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Danny's House of Curries",
                invoice_number = "#100000", /*Convert.ToString((new Random()).Next(100000)),*/
                amount = amount,
                item_list = listItems
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return this.payment.Create(apiContext);
        }

        // Create ExecutePayment method
        private object ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        // Create PaymentWithPayPal method
        public ActionResult PaymentWithPaypal()
        {
            // Getting context from the paypal bases on clientId and clientSecret for payment
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    // Creating a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/ShoppingCart/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000000));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    // Get links returned from paypal response to create call function
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This one will be executed when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.ToString().ToLower() != "approved")
                    {
                        // Remove shopping cart session
                        Session.Remove(strCart);
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                //PayPalLogger.Log("Error: " + ex.Message);

                // Remove shopping cart session
                Session.Remove(strCart);
                return View("Failure");
            }

            // Remove shopping cart session
            Session.Remove(strCart);
            return View("Success");
        }
    }
}
