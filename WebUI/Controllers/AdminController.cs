using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin

        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);

        }

        public ViewResult Edit(int productId)
        {

            System.Diagnostics.Debug.WriteLine("asdasdasdas" + productId.ToString());

            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {
                if (image != null)
                {

                    System.Diagnostics.Debug.WriteLine(image.FileName);
                    System.Diagnostics.Debug.WriteLine(image.ContentLength);





                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                
                }









                repository.SaveProduct(product);
                TempData["message"] = string.Format("zapisano {0}", product.Name);
                return RedirectToAction("Index");

            }
            else {


                return View(product);
            }

        }


        public ViewResult Create()
        {

            return View("Edit", new Product());
        }


        [HttpPost]
        public ActionResult Delete(int productId)
        {

            Product deleteproduct = repository.DeleteProduct(productId);
            if (deleteproduct != null)
            {
                TempData["message"] = string.Format("Usunieto {0}", deleteproduct.Name);
            
            }

            return RedirectToAction("Index");

        }

    }
}