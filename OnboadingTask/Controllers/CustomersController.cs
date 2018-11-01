using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnboadingTask;
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
            //string message = "SUCCESS";
            var cust = db.Customers.Add(customer);
            //try
            //{
            db.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
            //    {
            //        foreach (var validationError in entityValidationErrors.ValidationErrors)
            //        {
            //            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
            //        }
            //    }
            //}
            return Json(cust, JsonRequestBehavior.AllowGet );

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
           var removeCustomer =   db.Customers.Remove(customer);
            db.SaveChanges();
            return Json(removeCustomer, JsonRequestBehavior.AllowGet);
        }
        
       
    }
}
