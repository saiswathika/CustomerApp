﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerApp;

namespace CustomerApp.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ProductSoldsController : Controller
    {
        private CustomerDBEntities3 db = new CustomerDBEntities3();

        // GET: ProductSolds
        public ActionResult Index()
        {
            var productSolds = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store);
            return View(productSolds.ToList());
        }

        // GET: ProductSolds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }
        // GET: ProductSolds/Create
        public ActionResult Create()
        {
            using (var db = new CustomerDBEntities3())
            { 
                CustomerDBEntities3 entities = new CustomerDBEntities3();
                List<SelectListItem> ProductList = (from p in db.Products.AsEnumerable()
                                                     select new SelectListItem
                                                     {
                                                         Text = p.Name,
                                                         Value = p.Id.ToString()
                                                     }).ToList();
                ProductList.Insert(0, new SelectListItem { Text = "--Select Product--", Value = "" });
                List<SelectListItem> customerList = (from p in entities.Customers.AsEnumerable()
                                                     select new SelectListItem
                                                     {
                                                         Text = p.Name,
                                                         Value = p.Id.ToString()
                                                     }).ToList();
                customerList.Insert(0, new SelectListItem { Text = "--Select Customer--", Value = "" });
                List<SelectListItem> StoreList = (from p in entities.Stores.AsEnumerable()
                                                     select new SelectListItem
                                                     {
                                                         Text = p.Name,
                                                         Value = p.Id.ToString()
                                                     }).ToList();
                StoreList.Insert(0, new SelectListItem { Text = "--Select Store--", Value = "" });
                ViewBag.CurrencyList = ProductList;
                ViewBag.StoreList = StoreList;
                ViewBag.CustomerList = customerList;

                return View();
            }
     }
       [HttpPost]
        public ActionResult Create([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(productSold);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productSold);
        }
        // GET: ProductSolds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            List<SelectListItem> ProductList = (from p in db.Products.AsEnumerable()
                                                select new SelectListItem
                                                {
                                                    Text = p.Name,
                                                    Value = p.Id.ToString()
                                                }).ToList();
           List<SelectListItem> customerList = (from p in db.Customers.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.Name,
                                                     Value = p.Id.ToString()
                                                 }).ToList();
            List<SelectListItem> StoreList = (from p in db.Stores.AsEnumerable()
                                              select new SelectListItem
                                              {
                                                  Text = p.Name,
                                                  Value = p.Id.ToString()
                                              }).ToList();
            ViewBag.CustomerList = customerList;
            ViewBag.ProductList = ProductList;
            ViewBag.StoreList = StoreList;
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }

        // POST: ProductSolds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productSold);
        }
        // GET: ProductSolds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }

        // POST: ProductSolds/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSold productSold = db.ProductSolds.Find(id);
            db.ProductSolds.Remove(productSold);
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
