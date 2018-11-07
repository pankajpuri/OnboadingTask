using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnboadingTask.Models;

namespace OnboadingTask.Controllers
{
    public class StoresController : Controller
    {
        private OnBoad_2018Entities db = new OnBoad_2018Entities();

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Store store)
        {
            var stor = db.Stores.Add(store);
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
            return Json(stor, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetStore(string id)
        {
            List<Store> stores = new List<Store>();
            stores = db.Stores.ToList();
            return Json(stores, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            var store = db.Stores.ToList().Find(x => x.Id.Equals(ID));
            return Json(store, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Store store)
        {
            try
            {
                db.Entry(store).State = EntityState.Modified;

                return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return Json(db.SaveChanges(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Delete(int id)
        {
            Store store = db.Stores.Find(id);
            var removeStore = db.Stores.Remove(store);
            db.SaveChanges();
            return Json(removeStore, JsonRequestBehavior.AllowGet);
        }


    }
}
