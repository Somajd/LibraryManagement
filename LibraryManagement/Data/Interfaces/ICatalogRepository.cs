using LibraryManagement.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interfaces
{
    public interface ICatalogRepository: IRepository<Catalog>
    {
        IEnumerable<Catalog> GetAllEnabledPipeline();

        IEnumerable<Catalog> GetAllPublished();

        IEnumerable<Catalog> GetAllOrdered();

        bool OrderCatalog(Catalog catalog );

        IEnumerable<Catalog> GetCatalogWithPipelines(int id);
    }
}
