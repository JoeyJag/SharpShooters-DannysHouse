using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Sharpshooter.Models;
using Sharpshooter.ViewModel;
using Sharpshooter.PayPal;

namespace Sharpshooter.Controllers
{
    public class PayPalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }


        //private int isExisting(int id)
        //{
        //    List<Cart> cart = (List<Cart>) Session["cart"];
        //    for(int i = 0; i < cart.Count; i++)
        //    {
        //        if(cart[i].MenuItemID == id)
        //        {
        //            return i;
        //        }
                
        //    }
        //    return -1;
        //}

        //public ActionResult Delete(int id)
        //{
        //    int index = isExisting(id);
        //    List<Cart> cart = (List<Cart>)Session["cart"];
        //    cart.RemoveAt(index);
        //    Session["cart"] = cart;
        //    return View("Cart");
        //}

        //public ActionResult OrderNow(int id)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        List<Cart> cart = new List<Cart>();
        //        cart.Add(new Cart(db.MenuItems.Find(id), 1));
        //        Session["cart"] = cart;
        //    }
        //    else
        //    {
        //        List<Cart> cart = (List<Cart>)Session["cart"];
        //        int index = isExisting(id);
        //        if (index == -1)
        //        {
        //            cart.Add(new Cart(db.MenuItems.Find(id), 1));
        //        }
        //        else
        //        {
        //            //cart[index].MenuItem++;
        //            Session["cart"] = cart;
        //        }
        //        return View("Cart");
        //    }
        //}


        public ActionResult Success()
        {
            ViewBag.result = PDTHolder.Success(Request.QueryString.Get("tx"));
            return View("Success");
        }
    }
}