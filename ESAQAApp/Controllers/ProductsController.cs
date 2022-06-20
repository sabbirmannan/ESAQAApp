using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DOF003.Models;
using DOF003.Helpers;

namespace DOF003.Controllers
{
    [AccessDeniedAuthorize]
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/Products
        public IEnumerable<Product> Get()
        {
            return db.Products.AsEnumerable();
        }

        // GET api/Products/5
        public Product Get(int id)
        {
            return db.Products.FirstOrDefault(x => x.Id == id);
        }

        public Product Post(string name, double price, string description)
        {
            var product = new Product()
            {
                Name = name,
                Price = price,
                Description = description
            };

            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public Product Post(Product product)
        {
            var foundProduct = db.Products.FirstOrDefault(f => f.Id == product.Id);
            if (foundProduct != null)
            {
                foundProduct.Name = product.Name;
                foundProduct.Price = product.Price;
                foundProduct.Description = product.Description;
            }

            db.Entry(foundProduct).State = EntityState.Modified;
            //var x = db.SaveChanges();
            db.SaveChanges();
            return foundProduct;
        }

        public void Delete(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                db.Products.Remove(product);
            }
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}