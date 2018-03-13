using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PRS.Models;

namespace PRS.Controllers
{
    public class VendorsController : BaseCtrl
    {
        public ActionResult List()
        {
            return Success(db.Vendors.ToList());
        }

        public ActionResult Get(int? Id)
        {
            if (Id == null)
            {
                return Failure("Vendor ID is null.");
            }

            Vendor vendor = db.Vendors.Find(Id);

            if (vendor == null)
            {
                return Failure("Vendor ID does not exist.");
            }

            return Success(vendor);
        }

        public ActionResult Update(Vendor vendor)
        {
            Vendor oldVendor = db.Vendors.Find(vendor.Id);

            if (vendor == null)
            {
                return Failure("Unable to find this vendor ID");
            }

            oldVendor.Copy(vendor);
            oldVendor.UpdateTime();

            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Succesfully changed Vendor ID " + vendor.Id);
        }
        public ActionResult Remove(Vendor vendor)
        {
            Vendor existingVendor = db.Vendors.Find(vendor.Id);
            if (existingVendor == null)
            {
                return Failure("Unable to locate Vendor ID.");
            }

            db.Vendors.Remove(existingVendor);

            if (!Save())
            {
                return BadSaveResult();
            }

            return Success("Removed Vendor ID " + vendor.Id);
        }
        public ActionResult Create(Vendor vendor)
        {
            vendor.DateCreated = DateTime.UtcNow;
            vendor.UpdateTime();
            Vendor newVendor = db.Vendors.Add(vendor);
            if (newVendor == null)
            {
                return Failure("Unable to create new Vendor.");
            }
            if (!Save())
            {
                return BadSaveResult();
            }

            return Success("New Vendor created: ID " + newVendor.Id);
        }
    }
}