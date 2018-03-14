using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PRS.Models;

using System.Diagnostics;
using System.Web.Http;

namespace PRS.Controllers
{
    public class PurchaseRequestLineItemsController : BaseCtrl
    {
        public ActionResult List()
        {
            return Success(db.PurchaseRequestLineItems.ToList());
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Failure("Purchase Request line item ID is null.");
            }

            PurchaseRequestLineItem prli = db.PurchaseRequestLineItems.Find(id);

            if (prli == null)
            {
                return Failure("Purchase Request line item ID does not exist.");
            }

            return Success(prli);
        }

        public ActionResult Update(PurchaseRequestLineItem prli)
        {
            PurchaseRequestLineItem oldPRLI = db.PurchaseRequestLineItems.Find(prli.Id);

            if (prli == null)
            {
                return Failure("Unable to find this Purchase Request line item ID");
            }

            int id = oldPRLI.ProductID;
            oldPRLI.Copy(prli);
            oldPRLI.UpdateTime();

            if (!Save())
            {
                return BadSaveResult();
            }

            UpdateTotalForPurchaseRequest(id);

            if(!Save())
            {
                return BadSaveResult();
            }

            return Success("Succesfully changed Purchase Request line item id " + prli.Id);
        }

        public ActionResult Remove(PurchaseRequestLineItem prli)
        {
            PurchaseRequestLineItem existingPRLI = db.PurchaseRequestLineItems.Find(prli.Id);
            if (existingPRLI == null)
            {
                return Failure("Unable to locate Purchase Request line item ID.");
            }

            int id = existingPRLI.PurchaseRequestID;
            db.PurchaseRequestLineItems.Remove(existingPRLI);

            if (!Save())
            {
                return BadSaveResult();
            }

            UpdateTotalForPurchaseRequest(id);

            if(!Save())
            {
                return BadSaveResult();
            }

            return Success("Removed Purchase Request line item id " + prli.Id);
        }
        public ActionResult Create(PurchaseRequestLineItem prli)
        {
            prli.DateCreated = DateTime.UtcNow;
            prli.UpdateTime();
            PurchaseRequestLineItem newPRLI = db.PurchaseRequestLineItems.Add(prli);

            if (newPRLI == null)
            {
                return Failure("Unable to create new product.");
            }

            int newid = newPRLI.Id;
            int id = newPRLI.PurchaseRequestID;

            if (!Save())
            {
                return BadSaveResult();
            }

            UpdateTotalForPurchaseRequest(id);

            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("New Purchase Request line item created.");
        }

        //you have to manually save after calling this.
        private void UpdateTotalForPurchaseRequest(int id)
        {
            PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);

            purchaseRequest.Total =  db.PurchaseRequestLineItems
                .Join(db.Products, prli => prli.ProductID, product => product.Id, (prli, product) => new { prli, product })
                .Where(sc => sc.prli.PurchaseRequestID == id)
                .Select(sc => sc.prli.Product.Price * sc.prli.Quantity)
                .Sum();

            // db = new AppDbContext();
            /*
            List<PurchaseRequestLineItem> items = db.PurchaseRequestLineItems.Where(e => e.PurchaseRequestID == id).ToList();
            if (items == null){
                return;
            }
            decimal total = 0.00m;
            foreach (var i in items){
                total += (i.Product.Price * i.Quantity);
            }
            PurchaseRequest purchaseRequest = db.PurchaseRequests.Find(id);
            purchaseRequest.Total = total;
            */
        }
        
        public ActionResult GetTotal([FromBody] int? id)
        {
            if(id == null)
            {
                return Failure("No null id allowed");
            }
            decimal total = UpdateTotalPurchaseRequest2( (int)id );
            return Success("Total is " + total);
        }

        private decimal UpdateTotalPurchaseRequest2(int id)
        {
            return db.PurchaseRequestLineItems
                .Join(db.Products, prli => prli.ProductID, product => product.Id, (prli, product) => new { prli, product })
                .Where(sc => sc.prli.PurchaseRequestID == id)
                .Select(sc => sc.prli.Product.Price * sc.prli.Quantity)
                .Sum();
        }
    }
}