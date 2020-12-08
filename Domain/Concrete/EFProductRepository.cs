﻿using Domain.Abstract;
using Domain.Entities;
using System.Linq;

namespace Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products
        {
            get { return context.Products; }

        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);


            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                        
                
                }
            
            
            }

            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {

            Product dbentry = context.Products.Find(productID);

            if (dbentry != null)
            {

                context.Products.Remove(dbentry);
                context.SaveChanges();
            }

            return dbentry;
        }
    }
}
