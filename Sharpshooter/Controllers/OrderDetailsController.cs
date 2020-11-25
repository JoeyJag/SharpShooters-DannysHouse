using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sharpshooter.Models;

namespace Sharpshooter.Controllers
{
    public class OrderDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult ListPage()
        {
            var context = new ApplicationDbContext();
            var users = context.Users.ToList();

            return View(users);           
        }

        
        // GET: OrderDetails
        public ActionResult Index()
        {  // View(db.GameDetails.Where(x => x.Genre == search || search == null).ToList());
         
            var orderDetails = db.OrderDetails.Include(o => o.MenuItem).Include(o => o.Order);

            return View(orderDetails.Where(x => x.OrderStatus == false).ToList());
        }



        public ActionResult Delivery()
        {  // View(db.GameDetails.Where(x => x.Genre == search || search == null).ToList());

            var orderDetails = db.OrderDetails.Include(o => o.MenuItem).Include(o => o.Order);

            var DeliveryDetails = orderDetails.Where(x => x.OrderStatus == true).ToList();

            var DeliProg = DeliveryDetails.Where(x => x.DeliveryStatus == false).ToList();

            return View(DeliProg.Where(x => x.DeliveryProcess == true).ToList());
        }


        public ActionResult OpenDeliveries()
        {  // View(db.GameDetails.Where(x => x.Genre == search || search == null).ToList());

            var orderDetails = db.OrderDetails.Include(o => o.MenuItem).Include(o => o.Order);

            var DeliveryDetails = orderDetails.Where(x => x.OrderStatus == true).ToList();

            var DeliProg = DeliveryDetails.Where(x => x.DeliveryStatus == false).ToList();

            return View(DeliProg.Where(x => x.DeliveryProcess == false).ToList());
        }





        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.MenuItemID = new SelectList(db.MenuItems, "MenuItemID", "MenuItemTitle");
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetailId,OrderId,MenuItemID,OrderStatus,DeliveryStatus,DeliveryProcess,CurrentDeliveryProcess,Quantity,UnitPrice,DeliveryGuy")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.CurrentDeliveryProcess = orderDetail.getDeliveryProcess();

                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuItemID = new SelectList(db.MenuItems, "MenuItemID", "MenuItemTitle", orderDetail.MenuItemID);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderDetail.OrderId);
            return View(orderDetail);
        }




        public ActionResult DeliveryProgress(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName", orderDetail.DriverID);
            ViewBag.MenuItemID = new SelectList(db.MenuItems, "MenuItemID", "MenuItemTitle", orderDetail.MenuItemID);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderDetail.OrderId);
            return View(orderDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryProgress([Bind(Include = "OrderDetailId,OrderId,MenuItemID,DriverID,OrderStatus,DeliveryStatus,DeliveryProcess,CurrentDeliveryProcess,Quantity,UnitPrice,DeliveryGuy")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.CurrentDeliveryProcess = orderDetail.getDeliveryProcess();

                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OpenDeliveries");
            }
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName", orderDetail.DriverID);
            ViewBag.MenuItemID = new SelectList(db.MenuItems, "MenuItemID", "MenuItemTitle", orderDetail.MenuItemID);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderDetail.OrderId);
            return View(orderDetail);
        }








        public ActionResult DeliveryEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName", orderDetail.DriverID);
            ViewBag.MenuItemID = new SelectList(db.MenuItems, "MenuItemID", "MenuItemTitle", orderDetail.MenuItemID);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderDetail.OrderId);
            return View(orderDetail);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryEdit([Bind(Include = "OrderDetailId,OrderId,MenuItemID,DriverID,OrderStatus,DeliveryStatus,DeliveryProcess,CurrentDeliveryProcess,Quantity,UnitPrice,DeliveryGuy")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.CurrentDeliveryProcess = orderDetail.getDeliveryProcess();

                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Delivery");
            }
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName", orderDetail.DriverID);
            ViewBag.MenuItemID = new SelectList(db.MenuItems, "MenuItemID", "MenuItemTitle", orderDetail.MenuItemID);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderDetail.OrderId);
            return View(orderDetail);
        }






        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName", orderDetail.DriverID);
            ViewBag.MenuItemID = new SelectList(db.MenuItems, "MenuItemID", "MenuItemTitle", orderDetail.MenuItemID);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderDetail.OrderId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetailId,OrderId,MenuItemID,DriverID,OrderStatus,DeliveryStatus,DeliveryProcess,CurrentDeliveryProcess,Quantity,UnitPrice,DeliveryGuy")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.CurrentDeliveryProcess = orderDetail.getDeliveryProcess();

                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "DriverName", orderDetail.DriverID);
            ViewBag.MenuItemID = new SelectList(db.MenuItems, "MenuItemID", "MenuItemTitle", orderDetail.MenuItemID);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "Username", orderDetail.OrderId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
