using Domain.Abstract;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;
using Domain.Entities;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int pageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public FileContentResult GetImage(int productId)
        {
            System.Diagnostics.Debug.WriteLine("*******************************");
            System.Diagnostics.Debug.WriteLine("*******************************");


            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                System.Diagnostics.Debug.WriteLine("*******************************");
                System.Diagnostics.Debug.WriteLine(prod.ImageData);
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            { return null; }
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = repository.Products.Where(p=> category==null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?    repository.Products.Count() : repository.Products.Where(p=>p.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(viewModel);
        }
    }
}