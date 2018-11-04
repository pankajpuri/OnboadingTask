using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnboadingTask.Models;

namespace OnboadingTask.Controllers
{
    public class ProductSoldsController : Controller
    {
        private OnBoad_2018Entities db = new OnBoad_2018Entities();
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ProductSold productSold)
        {
            var cust = db.ProductSolds.Add(productSold);
            db.SaveChanges();
            return Json(cust, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCustomer(string id)
        {
            List<Customer> customers = new List<Customer>();
            customers = db.Customers.ToList();
            return Json(customers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            var customer = db.Customers.ToList().Find(x => x.Id.Equals(ID));
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Customer customer)
        {
            var changeCustomer = db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            return Json(changeCustomer, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            var removeCustomer = db.Customers.Remove(customer);
            db.SaveChanges();
            return Json(removeCustomer, JsonRequestBehavior.AllowGet);
        }


    }
}



//// GET: ProductSolds
//public ActionResult Index()
//        {
//            var productSolds = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store);
//            return View(productSolds.ToList());
//        }

//        // GET: ProductSolds/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ProductSold productSold = db.ProductSolds.Find(id);
//            if (productSold == null)
//            {
//                return HttpNotFound();
//            }
//            return View(productSold);
//        }

//        // GET: ProductSolds/Create
//        public ActionResult Create()
//        {
//            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
//            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
//            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name");
//            return View();
//        }

//        // POST: ProductSolds/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
//        {
//            if (ModelState.IsValid)
//            {
//                db.ProductSolds.Add(productSold);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", productSold.CustomerId);
//            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productSold.ProductId);
//            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", productSold.StoreId);
//            return View(productSold);
//        }

//        // GET: ProductSolds/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ProductSold productSold = db.ProductSolds.Find(id);
//            if (productSold == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", productSold.CustomerId);
//            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productSold.ProductId);
//            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", productSold.StoreId);
//            return View(productSold);
//        }

//        // POST: ProductSolds/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(productSold).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", productSold.CustomerId);
//            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productSold.ProductId);
//            ViewBag.StoreId = new SelectList(db.Stores, "Id", "Name", productSold.StoreId);
//            return View(productSold);
//        }

//        // GET: ProductSolds/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ProductSold productSold = db.ProductSolds.Find(id);
//            if (productSold == null)
//            {
//                return HttpNotFound();
//            }
//            return View(productSold);
//        }

//        // POST: ProductSolds/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            ProductSold productSold = db.ProductSolds.Find(id);
//            db.ProductSolds.Remove(productSold);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }

