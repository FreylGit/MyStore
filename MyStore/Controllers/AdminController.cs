using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Models.Interface;

namespace MyStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _productRepository;
        public AdminController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()=>View(_productRepository.Products);
        public ViewResult Edit(int productId)
        {
            return View(_productRepository.Products.FirstOrDefault(p => p.ProductId == productId));
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            
                _productRepository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            
            
        }
        public ViewResult Create() => View("Edit", new Product());
        
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = _productRepository.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
