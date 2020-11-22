using Sharpshooter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Sharpshooter.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Items()
        {
            var menuItems = db.MenuItems.Include(m => m.Menu).Include(m => m.MenuGroup);
            return View(menuItems.ToList());
        }

        public ActionResult Menus()
        {
            return View(db.Menus.ToList());
        }

        public ActionResult MenuGroups()
        {
            var menuGroups = db.MenuGroups.Include(m => m.Menu);
            return View(menuGroups.ToList());
        }
    }
}