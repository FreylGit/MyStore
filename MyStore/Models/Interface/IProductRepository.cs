namespace MyStore.Models.Interface
{
    public interface IProductRepository
    {
        public IQueryable<Product>Products { get; }

    }
}
