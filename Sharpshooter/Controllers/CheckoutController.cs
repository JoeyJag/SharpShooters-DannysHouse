using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sharpshooter.Models;
using Sharpshooter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sharpshooter.Controllers
{
        [Authorize]
        public class CheckoutController : Controller
        {
            ApplicationDbContext db = new ApplicationDbContext();
        

            public ActionResult AddressAndPayment()
            {
                return View();
            }

   

            [HttpPost]
            public ActionResult AddressAndPayment(FormCollection values)
            {
                var order = new Order();
                TryUpdateModel(order);

                try
                {
                    
                        order.Username = User.Identity.Name;
                        order.OrderDate = DateTime.Now;


                        db.Orders.Add(order);
                        db.SaveChanges();

                        var cart = ShoppingCart.GetCart(this.HttpContext);
                        cart.CreateOrder(order);

                        return RedirectToAction("Complete",
                            new { id = order.OrderId });
                    
                }
                catch
                {

                    return View(order);
                }
            }

        public ActionResult DetailsCustomer(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Order order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return PartialView(order);
            
        }

        public ActionResult DetailsOrder(int? id)
        {
            OrderDetail orderDetail = new OrderDetail();
            orderDetail = db.OrderDetails.Where(x => x.OrderId == id).FirstOrDefault();

            return PartialView("DetailsOrder", db.OrderDetails.Where(x => x.OrderId == id).ToList());
        }

        public ActionResult AllOrders()
        {
            return View(db.Orders.ToList());
        }

        public ActionResult Complete(int id)
            {
            Order order = new Order();
            order = db.Orders.Where(x => x.OrderId == id).FirstOrDefault();

                bool isValid = db.Orders.Any(
                    o => o.OrderId == id &&
                    o.Username == User.Identity.Name);

                if (isValid)
                {
                    return View(id);
                }
                else
                {
                    return View("Error");
                }
            }

    }
    }