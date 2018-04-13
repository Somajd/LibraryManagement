using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Repository
{
    public class CatalogRepository : Repository<Catalog>, ICatalogRepository
    {
        public CatalogRepository(LibraryDbContext context) :base(context)
        {
                
        }
        
        public IEnumerable<Catalog> GetAllEnabledPipeline()
        {
            return _context.Catalogs.Include(a => a.Pipelines) ;
        }

        public IEnumerable<Catalog> GetAllOrdered()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Catalog> GetAllPublished()
        {
            yield return _context.Catalogs
                            .Where(a => a.PublishedStatus == ePublishedStatus.Yes)
                            .Include(a => a.Pipelines)
                            .FirstOrDefault();
        }

        public IEnumerable<Catalog> GetCatalogWithPipelines(int id)
        {
            //throw new NotImplementedException();
            return _context.Catalogs
                .Where( a => a.CatalogId == id)
                    .Include(a => a.Pipelines);
        }

            public bool OrderCatalog(Catalog catalog)
        {
            throw new NotImplementedException();
            //Order or = new Order();
            //or.Catalog = _context.Catalogs.

            //_context.Orders.Add()

            //return true;
        }
    }
}
