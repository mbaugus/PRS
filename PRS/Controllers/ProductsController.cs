using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PRS.Models;
namespace PRS.Controllers
{

    public class ProductsController : BaseCtrl
    {
        public ActionResult List()
        {
            return Success(db.Products.ToList());
        }

        public ActionResult Get(int? id)
        {
            if(id == null)
            {
                return Failure("Product id is null.");
            }

            Product product = db.Products.Find(id);

            if(product == null)
            {
                return Failure("Product ID does not exist.");
            }

            return Success(product);
        }

        public ActionResult Update(Product product)
        {
            Product oldProduct = db.Products.Find(product.Id);

            if(product == null)
            {
                return Failure("Unable to find this product ID");
            }

            oldProduct.Copy(product);
            oldProduct.UpdateTime();
            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Succesfully changed product id " + product.Id);
        }
        public ActionResult Remove(Product product)
        {
            Product existingProduct = db.Products.Find(product.Id);
            if(existingProduct == null)
            {
                return Failure("Unable to locate product ID.");
            }
         
            db.Products.Remove(existingProduct);

            if (!Save())
            {
                return BadSaveResult();
            }

            return Success("Removed product id " + product.Id);
        }
        public ActionResult Create(Product product)
        {
            product.DateCreated = DateTime.UtcNow;
            product.UpdateTime();
            Product newProduct = db.Products.Add(product);
            if(newProduct == null)
            {
                return Failure("Unable to create new product.");
            }
            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("New product created: Id " + newProduct.Id);
        }
    }
}