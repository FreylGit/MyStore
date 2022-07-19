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
        public ViewResult List() =>
            View(_orderRepository.Orders.Where(o => !o.Shipped));
        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = _orderRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Shipped = true;
                _orderRepository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }
        public ViewResult Checkout()
        {
            var order = new Order();
            order.Lines = _cart.Lines.ToArray();
            return View(order);
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {

            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");
            }

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
