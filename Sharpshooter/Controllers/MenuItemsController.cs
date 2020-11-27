using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sharpshooter.Models;
using System.IO;

namespace Sharpshooter.Controllers
{
    public class MenuItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MenuItems
        public ActionResult Index()
        {
            var menuItems = db.MenuItems.Include(m => m.Menu).Include(m => m.MenuGroup);
            return View(menuItems.ToList());
        }


        public ActionResult ViewMenuItems()
        {
            var menuItems = db.MenuItems.Include(m => m.Menu).Include(m => m.MenuGroup);
            return View(menuItems.ToList());
        }

        // GET: MenuItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // GET: MenuItems/Create
        public ActionResult Create()
        {
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "MenuTitle");
            ViewBag.MenuGroupID = new SelectList(db.MenuGroups, "MenuGroupID", "MenuGroupTitle");
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuItemID,MenuID,MenuGroupID,MenuItemTitle,MenuItemDescription,MenuItemNutrition,MenuItemIngredients,MenuItemQuantity,MenuItemImg,ImageFile,MenuItemCost")] MenuItem menuItem/*, HttpPostedFileBase MenuItemImg*/)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(menuItem.ImageFile.FileName);
                string extension = Path.GetExtension(menuItem.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                menuItem.MenuItemImg = "~/Content/ItemImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/ItemImages/"), fileName);
                menuItem.ImageFile.SaveAs(fileName);



                //MenuItem m = new MenuItem();
                //m.MenuItemTitle = menuItem.MenuItemTitle;
                //m.MenuItemImg = MenuItemImg.FileName.ToString();

                //var path = Server.MapPath("~/Content/ItemImages/");
                //MenuItemImg.SaveAs(Path.Combine(path, MenuItemImg.FileName.ToString()));

                db.MenuItems.Add(menuItem);
                db.SaveChanges();
                return RedirectToAction("Items", "Admin");
            }

            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "MenuTitle", menuItem.MenuID);
            ViewBag.MenuGroupID = new SelectList(db.MenuGroups, "MenuGroupID", "MenuGroupTitle", menuItem.MenuGroupID);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "MenuTitle", menuItem.MenuID);
            ViewBag.MenuGroupID = new SelectList(db.MenuGroups, "MenuGroupID", "MenuGroupTitle", menuItem.MenuGroupID);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuItemID,MenuID,MenuGroupID,MenuItemTitle,MenuItemDescription,MenuItemNutrition,MenuItemIngredients,MenuItemQuantity,MenuItemImg,ImageFile,MenuItemCost")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(menuItem.ImageFile.FileName);
                string extension = Path.GetExtension(menuItem.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                menuItem.MenuItemImg = "~/Content/ItemImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/ItemImages/"), fileName);
                menuItem.ImageFile.SaveAs(fileName);


                db.Entry(menuItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuID = new SelectList(db.Menus, "MenuID", "MenuTitle", menuItem.MenuID);
            ViewBag.MenuGroupID = new SelectList(db.MenuGroups, "MenuGroupID", "MenuGroupTitle", menuItem.MenuGroupID);
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItem menuItem = db.MenuItems.Find(id);
            db.MenuItems.Remove(menuItem);
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
