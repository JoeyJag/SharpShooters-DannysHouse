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
    public class MenusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Menus
        public ActionResult Index()
        {
            return View(db.Menus.ToList());
        } 

        public ActionResult Menu()
        {
            var categories = db.Menus.ToList();
            return View(categories);
        }

        public ActionResult ViewMenuAll()
        {
            var categories = db.Menus.ToList();
            return View(categories);
        }


        public ActionResult Browse(string category)
        {
            var categoryModel = db.Menus.Include("MenuItems").Single(c => c.MenuTitle == category);
            return View(categoryModel);
            
        }
        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        public ActionResult ViewItems(int id)
        {
            MenuItem menuItem = new MenuItem();
            menuItem = db.MenuItems.Where(x => x.MenuItemID == id).FirstOrDefault();
            
            return View(menuItem);
        }

        public ActionResult ViewMenu(int id)
        {
            var Item = db.MenuItems.Find(id);
            return View(Item);
        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuID,MenuTitle,MenuDescription,MenuImg")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuID,MenuTitle,MenuDescription,MenuImg")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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

        public ActionResult MenuReview(int? id)
        {
            Review review = new Review();
            review = db.Reviews.Where(x => x.MenuItemID == id).FirstOrDefault();
            return PartialView("MenuReview", db.Reviews.Where(x => x.MenuItemID == id).ToList());
        }
        public ActionResult CalcAvg(int? id)
        {
            int num = 0;
            float Average = 0;
            Review review = new Review();
            review = db.Reviews.Where(x => x.MenuItemID == id).FirstOrDefault();
            foreach (var i in db.Reviews.Where(x => x.MenuItemID == id).ToList())
            {
                Average = (Average + review.Rating);
                num = num + 1;
            }
            Average = Average / num;

            return View(Average);

        }
    }
}
