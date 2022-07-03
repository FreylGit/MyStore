using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Models.Interface;

namespace MyStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private Cart _cart;
        public OrderController(IOrderRepository orderRepository, Cart cart)
        {
            _orderRepository = orderRepository;
            _cart = cart;
        }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");
            }

            order.Lines = _cart.Lines.ToArray();
            _orderRepository.SaveOrder(order);
            return RedirectToAction(nameof(Completed));

        }
        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
