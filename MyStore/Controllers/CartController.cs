using Microsoft.AspNetCore.Mvc;
using MyStore.Infrastructure;
using MyStore.Models;
using MyStore.Models.Interface;
using MyStore.Models.ViewModels;

namespace MyStore.Controllers
{
    public class CartController : Controller
    {
        private  IProductRepository _repository;
        private Cart _cart;
        public CartController(IProductRepository repository, Cart cart)
        {
            _repository = repository;
            _cart = cart;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }
    
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {             
                _cart.AddItem(product, 1);
            }
            return RedirectToAction("Index","Cart", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId,string returnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {  
                _cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        /*
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }*/
    }
}
