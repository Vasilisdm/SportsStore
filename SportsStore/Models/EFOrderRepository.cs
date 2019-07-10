using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;

        public EFOrderRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Order> Orders => _context.Orders
                                                   .Include(o => o.Lines)
                                                   .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
