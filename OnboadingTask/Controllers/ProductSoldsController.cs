using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnboadingTask.Models;
using OnboadingTask.ViewModel;

namespace OnboadingTask.Controllers
{

 
    public class ProductSoldsController : Controller
    {
        private OnBoad_2018Entities db = new OnBoad_2018Entities();
        // GET: ProductSolds
        // GET: ProductSoldsz
        public ActionResult Index()
        {
            var model = new SalesViewModel()
            {
                Customers = db.Customers.ToList(),
                Products = db.Products.ToList(),
                Stores = db.Stores.ToList(),
                SalesList = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SalesViewModel model)
        {
            if (Convert.ToDateTime(model.ProductSold.DateSold) > DateTime.Now)
            {
                return Json(false);
            }
            if (model.ProductSold.Id > 0)
            {
                ProductSold ps = db.ProductSolds.Where(c => c.Id == model.ProductSold.Id).SingleOrDefault();
                ps.CustomerId = model.ProductSold.CustomerId;
                ps.ProductId = model.ProductSold.ProductId;
                ps.StoreId = model.ProductSold.StoreId;
                ps.DateSold = model.ProductSold.DateSold;
                db.SaveChanges();
            }
            else
            {
                ProductSold ps = new ProductSold()
                {
                    CustomerId = model.ProductSold.CustomerId,
                    ProductId = model.ProductSold.ProductId,
                    StoreId = model.ProductSold.StoreId,
                    DateSold = model.ProductSold.DateSold
                };

                db.ProductSolds.Add(ps);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // Delete Sale
        public ActionResult DeleteSale(int saleId)
        {
            bool result = false;
            ProductSold sale = db.ProductSolds.SingleOrDefault(x => x.Id == saleId);
            if (sale != null)
            {
                db.ProductSolds.Remove(sale);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // Update Sale
        public ActionResult EditSale(int saleId)
        {
            ProductSold ps = db.ProductSolds.Where(c => c.Id == saleId).SingleOrDefault();
            List<Customer> list = db.Customers.ToList();
            ViewBag.CustomerList = new SelectList(list, "Id", "Name");
            List<Product> list2 = db.Products.ToList();
            ViewBag.ProductList = new SelectList(list2, "Id", "Name");
            List<Store> list3 = db.Stores.ToList();
            ViewBag.StoreList = new SelectList(list3, "Id", "Name");
            SalesViewModel model = new SalesViewModel()
            {
                ProductSold = ps
            };
            return PartialView("EditSale", model);
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
//    public class ProductSoldsController : Controller
//    {

//        private OnBoad_2018Entities db = new OnBoad_2018Entities();

//        // GET: ProductSolds
//        public ActionResult Index()
//        {
//            var productSolds = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store);
//           return Json(productSolds.ToList(), JsonRequestBehavior.AllowGet);
//        }
//        public JsonResult List()
//        {
//            var productsold = db.ProductSolds.Include(s => s.Customer).Include(s => s.Product).Include(s => s.Store).Select(x => new
//            {
//                Id = x.Id,
//                ProductId = x.Product.Name,
//                CustomerId = x.Customer.Name,
//                StoreId = x.Store.Name,
//                DateSold = x.DateSold
//            }).ToList();

//            return Json(productsold, JsonRequestBehavior.AllowGet);
//        }
//        [HttpPost]
//        public ActionResult Add(ProductSold productSold)
//        {
//            var cust = db.ProductSolds.Add(productSold);
//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            return Json(cust, JsonRequestBehavior.AllowGet);

//        }
//        //var productSolds = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store);
//        //    return View(productSolds.ToList());
//        //public JsonResult GetSales(string id)
//        //{

//        //}
//            // GET: ProductSolds/Details/5
//            public ActionResult Details(int? id)
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
//}
