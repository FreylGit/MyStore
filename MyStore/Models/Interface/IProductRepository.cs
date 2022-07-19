namespace MyStore.Models.Interface
{
    public interface IProductRepository
    {
        public IQueryable<Product>Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productId);
    }
}
