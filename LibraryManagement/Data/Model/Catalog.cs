using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Model
{
    public enum ePublishedStatus
    {
        Yes,
        No
    }

    public class Catalog
    {
        public int CatalogId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public ePublishedStatus PublishedStatus { get; set; }
                
        public  ICollection<Pipeline> Pipelines { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
