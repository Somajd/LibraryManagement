using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(LibraryDbContext context) : base(context)
        {
        }

        public IEnumerable<Order> GetAllWithCatalog(int CatalogId)
        {
            //  throw new NotImplementedException();
            return _context.Orders
                .Include(a => a.Catalog.CatalogId == CatalogId);

                
        }

        public IEnumerable<Order> GetOrderWithCatalog(int OrderId)
        {
            //  throw new NotImplementedException();
            return _context.Orders
                .Where(a => a.OrderId == OrderId )
                .Include(a => a.Catalog);


        }

        public IEnumerable<Order> GetAllAvailableCatalog()
        {
            return _context.Orders.Include(a => a.Catalog);
        }
    }
}
