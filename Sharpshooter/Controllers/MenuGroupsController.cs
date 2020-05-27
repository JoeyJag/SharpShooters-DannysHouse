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
    public class MenuGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MenuGroups
        public ActionResult Index()
        {
            var menuGroups = db.MenuGroups.Include(m => m.Menu);
            return View(menuGroups.ToList());
        }

        public ActionResult ViewMenuGroups()
        {
            var menuGroups = db.MenuGroups.Include(m => m.Menu);
            return View(menuGroups.ToList());
        }

        // GET: MenuGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuGroup menuGroup = db.MenuGroups.Find(id);
            if (menuGroup == null)
            {
                return HttpNotFound();
            }
            return View(menuGroup);
        }

        // GET: MenuGroups/Create
        public ActionResult Create()
        {
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "MenuTitle");
            return View();
        }

        // POST: MenuGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuGroupID,MenuID,MenuGroupTitle,MenuGroupDescription")] MenuGroup menuGroup)
        {
            if (ModelState.IsValid)
            {
                db.MenuGroups.Add(menuGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "MenuTitle", menuGroup.MenuID);
            return View(menuGroup);
        }

        // GET: MenuGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuGroup menuGroup = db.MenuGroups.Find(id);
            if (menuGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "MenuTitle", menuGroup.MenuID);
            return View(menuGroup);
        }

        // POST: MenuGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuGroupID,MenuID,MenuGroupTitle,MenuGroupDescription")] MenuGroup menuGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "MenuTitle", menuGroup.MenuID);
            return View(menuGroup);
        }

        // GET: MenuGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuGroup menuGroup = db.MenuGroups.Find(id);
            if (menuGroup == null)
            {
                return HttpNotFound();
            }
            return View(menuGroup);
        }

        // POST: MenuGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuGroup menuGroup = db.MenuGroups.Find(id);
            db.MenuGroups.Remove(menuGroup);
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
