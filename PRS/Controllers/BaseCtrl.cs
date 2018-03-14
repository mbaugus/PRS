using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PRS.Models;
using PRS.Utility;

namespace PRS.Controllers
{
    /// <summary>
    ///  addon helper functions for all our controllers
    /// </summary>
    /// 
    public class BaseCtrl : Controller
    {
        protected AppDbContext db = new AppDbContext();
        protected Exception SaveException = null;
        protected ActionResult BadSaveResult() { return Js(new { Status = "Failure", Message = SaveException.Message + " " + SaveException.InnerException }); }
        protected ActionResult Success(object data) { return Js(data); }
        protected ActionResult Success(string Message) { return Js(new { Status = "Success", Message }); }
        protected ActionResult Failure(string Message) { return Js(new { Status = "Failure", Message }); }
        //protected ActionResult Js(object data) { return Json(data, JsonRequestBehavior.AllowGet); }

        protected ActionResult Js(object data)
        {
            return new JsonNetResult { Data = data };
        }
        protected bool Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                SaveException = e;
                return false;
            }
            return true;
        }
        
        protected decimal GetPurchaseRequestLineItemTotal(int id)
        {
            return 0;
            //db.PurchaseRequestLineItems.AddRange()
           // return 0;
        }
    }
}