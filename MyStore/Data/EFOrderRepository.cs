using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Models.Interface;

namespace MyStore.Data
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;
        public EFOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);//296 строка

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();


        }
    }
}
