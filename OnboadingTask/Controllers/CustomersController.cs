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
    public class CustomersController : Controller
    {
        private OnBoad_2018Entities db = new OnBoad_2018Entities();

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            var cust = db.Customers.Add(customer);
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
            return Json(cust, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCustomer(string id)
        {
            var customers = db.Customers.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address

            }).ToList();

            return Json(customers, JsonRequestBehavior.AllowGet);
           
        }
        public JsonResult GetbyID(int ID)
        {
            var customer = db.Customers.ToList().Find(x => x.Id.Equals(ID));
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Customer customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;

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
            Customer customer = db.Customers.Find(id);
            var removeCustomer = db.Customers.Remove(customer);
            db.SaveChanges();
            return Json(removeCustomer, JsonRequestBehavior.AllowGet);
        }


    }
}