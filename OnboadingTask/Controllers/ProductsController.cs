using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using OnboadingTask.Models;
using System.Data.Entity.Infrastructure;

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
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
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
            try
            {
                db.Entry(product).State = EntityState.Modified;

                return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
            //var changedProduct = db.Entry(product).State = EntityState.Modified;
            //var productFromDB = db.Products.Where(p => p.Id == product.Id).SingleOrDefault();
            //productFromDB.Name = product.Name;
            //productFromDB.Price = product.Price;

            //db.SaveChanges();
            //return Json(changedProduct, JsonRequestBehavior.AllowGet);
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

