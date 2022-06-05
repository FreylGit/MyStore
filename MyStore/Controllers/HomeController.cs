using Microsoft.AspNetCore.Mvc;
using MyStore.Models.Interface;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _repository;
        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Products);
        }
    }
}
