using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PRS.Models;

namespace PRS.Controllers
{
    public class PurchaseRequestsController : BaseCtrl
    {
        public ActionResult List()
        {
            return Success(db.PurchaseRequests.ToList());
        }

        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Failure("Purchase Request id is null.");
            }

            PurchaseRequest PurchaseRequest = db.PurchaseRequests.Find(id);

            if (PurchaseRequest == null)
            {
                return Failure("Purchase Request ID does not exist.");
            }

            return Success(PurchaseRequest);
        }

        public ActionResult Update(PurchaseRequest purchaseRequest)
        {
            PurchaseRequest oldPurchaseRequest = db.PurchaseRequests.Find(purchaseRequest.Id);

            if (oldPurchaseRequest == null)
            {
                return Failure("Unable to find this Purchase Request ID");
            }

            oldPurchaseRequest.Copy(purchaseRequest);
            oldPurchaseRequest.UpdateTime();

            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Succesfully changed Purchase Request id " + purchaseRequest.Id);
        }

        public ActionResult Remove(PurchaseRequest purchaseRequest)
        {
            PurchaseRequest existingPurchaseRequest = db.PurchaseRequests.Find(purchaseRequest.Id);
            if (existingPurchaseRequest == null)
            {
                return Failure("Unable to locate Purchase Request ID.");
            }

            db.PurchaseRequests.Remove(existingPurchaseRequest);

            if (!Save())
            {
                return BadSaveResult();
            }

            return Success("Removed Purchase Request id " + purchaseRequest.Id);
        }
        public ActionResult Create(PurchaseRequest purchaseRequest)
        {
            purchaseRequest.DateCreated = DateTime.UtcNow;
            purchaseRequest.UpdateTime();
            PurchaseRequest newPurchaseRequest = db.PurchaseRequests.Add(purchaseRequest);
            if (newPurchaseRequest == null)
            {
                return Failure("Unable to create new Purchase Request.");
            }

            if (!Save())
            {
                return BadSaveResult();
            }

            return Success("New Purchase Request created: Id " + newPurchaseRequest.Id);
        }
    }
}