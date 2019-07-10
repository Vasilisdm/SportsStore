using System;
using System.Linq;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        public IQueryable<Order> Orders => throw new NotImplementedException();

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
