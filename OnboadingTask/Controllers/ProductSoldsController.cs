using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using OnboadingTask.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace OnboadingTask.Controllers
{

    public class ProductSoldsController : Controller
    {
        public class ProductSoldViewModel
        {

            public int Id { get; set; }
            [Display(Name = "Product")]
            public long ProductId { get; set; }
            [Display(Name = "Customer")]
            public long CustomerId { get; set; }
            [Display(Name = "Store")]
            public int StoreId { get; set; }
            [Display(Name = "Date of Purchase")]
            // [DisplayFormat(DataFormatString ="{0:mm/dd/yyyy}", ApplyFormatInEditMode =true)]
            public DateTime DateSold { get; set; }
        }
        private OnBoad_2018Entities db = new OnBoad_2018Entities();
        // GET: Index
        // GET: ProductSolds
        public ActionResult Index()
        {
            var psold = new ProductSold();
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", psold.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", psold.ProductId);
            ViewBag.StoreId = new SelectList(db.Stores, "Id", "StoreName", psold.StoreId);
            return View();
        }


        public JsonResult List()
        {
            var productsold = db.ProductSolds.Include(s => s.Customer).Include(s => s.Product).Include(s => s.Store).Select(x => new
            {
                Id = x.Id,
                ProductId = x.Product.Name,
                CustomerId = x.Customer.Name,
                StoreId = x.Store.Name,
                DateSold = x.DateSold
            }).ToList();

            return Json(productsold, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(ProductSold psold)
        {


            db.ProductSolds.Add(psold);
            db.SaveChanges();

            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetbyID(int ID)
        {
            var ProductSold = db.ProductSolds.Where(x => x.Id == ID).Select(x => new ProductSoldViewModel
            {
                Id = ID,
                CustomerId = x.CustomerId,
                DateSold = x.DateSold,
                StoreId = x.StoreId,
                ProductId = x.ProductId
            }).FirstOrDefault();
            return Json(ProductSold, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(ProductSold psold)
        {
            try
            {
                db.Entry(psold).State = EntityState.Modified;
                return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RemoveProduct(long ID)
        {
            ProductSold psold = db.ProductSolds.Find(ID);
            db.ProductSolds.Remove(psold);
            return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
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

