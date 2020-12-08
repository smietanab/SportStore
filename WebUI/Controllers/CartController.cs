using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        IProductRepository repository;
        IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;

            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(Cart cart , int productid, string returnUrl)
        {
            System.Diagnostics.Debug.WriteLine(productid.ToString() + "  " + returnUrl);
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productid);

            if(product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index","Cart", new { returnUrl } );
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productid, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productid);
            if(product != null)
            {
                cart.RemoveLine(product);

            }
            return RedirectToAction("Index", new { returnUrl });
        
        }

        public ViewResult Index(Cart cart, string returnUrl)
        
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl});
        }


        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }



        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Koszyk jest pusty");
            }
            if (ModelState.IsValid)
            {

                orderProcessor.ProcessorOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }


        }


        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

    }
}