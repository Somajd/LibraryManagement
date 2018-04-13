using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Repository
{
    public class OrderLogRepository : Repository<OrderLog>, IOrderLogRepository
    {
        public OrderLogRepository(LibraryDbContext context) : base(context)
        {
        }

        public IEnumerable<OrderLog> GetAllWithCatalog(int CatalogId)
        {
            return _context.OrderLogs
                .Include(a => a.Order)
                .Where(a => a.Order.CatalogId == CatalogId);
        }
    }
}
