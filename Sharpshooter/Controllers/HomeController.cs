﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Sharpshooter.Models;

namespace Sharpshooter.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult DateSales()
        {
            return View();
        }

        public ActionResult SalesChart()
        {
            return View();
        }

        public ActionResult GetDataDate()
        {

            ApplicationDbContext context = new ApplicationDbContext();

            var query = context.OrderDetails.Include("Orders")
                .GroupBy(p => p.Order.OrderDate.ToString())
                .Select(g => new { name = g.Key, count = g.Sum(w => w.OrderId) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetData()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var query = context.OrderDetails.Include("MenuItem")
                .GroupBy(p => p.MenuItem.MenuItemTitle)
                .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FoodSold()
        {
            return PartialView("FoodSold", db.OrderDetails.ToList());
        }

        public ActionResult DSales(DateTime? start, DateTime? end)
        {
            var Dates = db.Orders.Where(t => t.OrderDate >= start && t.OrderDate <= end).ToList();
            return PartialView("DSales", Dates);
        }

        public ActionResult ViewDateSale(DateTime? start, DateTime? end)
        {
            var Dates = db.Orders.Where(t => t.OrderDate >= start && t.OrderDate <= end).ToList();

            return View(Dates);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AdminPanel()
        {
            ViewBag.Message = "Admin Panel.";

            return View();
        }
    }
}