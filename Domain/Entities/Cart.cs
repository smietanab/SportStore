﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int ilosc)
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = ilosc });
            }
            else
            {
                line.Quantity += ilosc;
            }
        }

        public void RemoveLine(Product product)
        {

            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(p => p.Product.Price * p.Quantity);
       
        }
        public void Clear()
        { lineCollection.Clear(); }

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }


    }


    public class CartLine
    { 
        public Product Product { get; set; }
        public int Quantity { get; set; }
    
    
    }
}
