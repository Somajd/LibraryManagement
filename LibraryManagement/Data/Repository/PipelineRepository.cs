using LibraryManagement.Data.Interfaces;
using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Repository
{
    public class PipelineRepository : Repository<Pipeline>, IPipelineRepository
    {
        public PipelineRepository(LibraryDbContext context) : base(context)
        {
        }

        public IEnumerable<Pipeline> GetAllWithCatalog(int CatalogId)
        {
            //  throw new NotImplementedException();
            return _context.Pipelines
                  .Where(a => a.CatalogId == CatalogId);
        }
    }
}
