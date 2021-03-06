using MyStore.Models;
using MyStore.Models.Interface;

namespace MyStore.Data
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;
        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Product> Products => _context.Products;

        public Product DeleteProduct(int productId)
        {
            Product product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if(product!= null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return product;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntity = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if(dbEntity != null)
                {
                    dbEntity.Name = product.Name;
                    dbEntity.Description = product.Description;
                    dbEntity.Price = product.Price;
                    dbEntity.Category = product.Category;
                }
            }
            _context.SaveChanges();
        }
    }
}
