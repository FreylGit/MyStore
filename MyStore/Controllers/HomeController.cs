using Microsoft.AspNetCore.Mvc;
using MyStore.Models.Interface;
using MyStore.Models.ViewModels;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize = 4;
        private IProductRepository _repository;
        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index(string category, int productPage = 1)
              => View(new ProductListViewModel
              {
                  Products = _repository.Products.Where(p => category == null || p.Category == category)
                      .OrderBy(p => p.ProductId)
                      .Skip((productPage - 1) * PageSize)
                      .Take(PageSize),
                  PagingInfo = new PagingInfo
                  {
                      CurrentPage = productPage,
                      ItemsPerPage = PageSize,
                      TotalItems = category == null ? _repository.Products.Count() : _repository.Products.Where(e =>
                                                                                            e.Category == category).Count()
                  },
                  CurrentCategory = category

              });
    }
}




