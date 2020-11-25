using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sharpshooter.Models;

namespace Sharpshooter.Controllers
{
    public class DriversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Drivers
        public ActionResult Index()
        {
            return View(db.Drivers.ToList());
        }

        public ActionResult DeliveryUser()
        {
            var delivery = db.Drivers.ToList();
            return View(delivery);
        }

        public ActionResult BrowseDelivery(string category)
        {
            var categoryModel = db.Drivers.Include("OrderDetails").Single(c => c.DriverName == category);
            return View(categoryModel);

        }

        public ActionResult ViewOrder(int id)
        {
            OrderDetail orderDetail = new OrderDetail();
            orderDetail = db.OrderDetails.Where(x => x.OrderId == id).FirstOrDefault();

            return View(orderDetail);
        }

        // GET: Drivers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drivers drivers = db.Drivers.Find(id);
            if (drivers == null)
            {
                return HttpNotFound();
            }
            return View(drivers);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverID,DriverName,DriverSurname,DriverVehicle,DriverIdNo")] Drivers drivers)
        {
            if (ModelState.IsValid)
            {
                db.Drivers.Add(drivers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drivers);
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drivers drivers = db.Drivers.Find(id);
            if (drivers == null)
            {
                return HttpNotFound();
            }
            return View(drivers);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverID,DriverName,DriverSurname,DriverVehicle,DriverIdNo")] Drivers drivers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drivers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drivers);
        }

        // GET: Drivers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drivers drivers = db.Drivers.Find(id);
            if (drivers == null)
            {
                return HttpNotFound();
            }
            return View(drivers);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Drivers drivers = db.Drivers.Find(id);
            db.Drivers.Remove(drivers);
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
