using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnboadingTask;
using OnboadingTask.Models;

namespace OnboadingTask.Controllers
{
    public class ProductsController : Controller
    {
        private OnBoad_2018Entities db = new OnBoad_2018Entities();

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product product)
        {
            var prod = db.Products.Add(product);
            db.SaveChanges();
            return Json(prod, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProduct(string id)
        {
            List<Product> products = new List<Product>();
            products = db.Products.ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            var product = db.Products.ToList().Find(x => x.Id.Equals(ID));
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Product product)
        {
            var changedProduct = db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return Json(changedProduct, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            var removeProduct = db.Products.Remove(product);
            db.SaveChanges();
            return Json(removeProduct, JsonRequestBehavior.AllowGet);
        }


    }
}

