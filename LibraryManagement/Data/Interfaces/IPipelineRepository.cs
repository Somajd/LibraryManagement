using LibraryManagement.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interfaces
{
    public interface IPipelineRepository : IRepository<Pipeline>
    {
        IEnumerable<Pipeline> GetAllWithCatalog(int CatalogId);
    }
}
