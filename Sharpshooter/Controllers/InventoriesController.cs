using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Sharpshooter.Models;

namespace Sharpshooter.Controllers
{
    public class InventoriesController : Controller
    {
        //public ActionResult SendEmail()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult SendEmail(string receiver, string subject, string message)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            ViewBag.Success = "Success, Manger has been informed";
        //            var senderEmail = new MailAddress("taslyn.moopanar@gmail.com", "Employee");
        //            var receiverEmail = new MailAddress("tasmoop@gmail.com", "Receiver");
        //            var password = "B@dboy2968";
        //            var sub = subject;
        //            var body = message;
        //            var smtp = new SmtpClient
        //            {
        //                Host = "smtp.gmail.com",
        //                Port = 587,
        //                EnableSsl = true,
        //                DeliveryMethod = SmtpDeliveryMethod.Network,
        //                UseDefaultCredentials = false,
        //                Credentials = new NetworkCredential(senderEmail.Address, password)
        //            };
        //            using (var mess = new MailMessage(senderEmail, receiverEmail)
        //            {
        //                Subject = subject,
        //                Body = body
        //            })
        //            {
        //                smtp.Send(mess);
        //            }
        //            return View();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.Error = "Error";
        //    }
        //    return View();
        //}



        private ApplicationDbContext db = new ApplicationDbContext();


        //filter code
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            ViewBag.QuantityRemainingSortParm = sortOrder == "QuantityRemaining" ? "quantityRemaining_asc" : "QuantityRemaining";

            var inventory = from s in db.Inventories
                            select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                inventory = inventory.Where(s => s.Name.Contains(searchString));

            }



            switch (sortOrder)
            {
                case "name_asc":
                    inventory = inventory.OrderByDescending(s => s.Name);
                    break;
                case "Type":
                    inventory = inventory.OrderBy(s => s.Type);
                    break;
                case "quantityRemaining_asc":
                    inventory = inventory.OrderBy(s => s.QuantityRemaining);
                    break;

                default:
                    inventory = inventory.OrderBy(s => s.Name);
                    break;
            }
            return View(inventory.ToList());
        }

        //low stock code
        public ViewResult lowStock(string sortOrder, string searchString)
        {
            var inventory = from s in db.Inventories
                            select s;

            return View(inventory.Where(i => (i.Name.Equals("Fanta") && i.QuantityRemaining < 10) || (i.Name.Equals("Mutton") && i.QuantityRemaining < 30) || (i.Name.Equals("Beans") && i.QuantityRemaining < 40)));
        }

        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Type,QuantityRemaining,PricePerUnit")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Inventories.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Type,QuantityRemaining,PricePerUnit")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
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
