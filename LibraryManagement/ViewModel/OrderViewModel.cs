using LibraryManagement.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModel
{
    public class OrderViewModel
    {
        public Catalog Catalog { get; set; }
        public Order Order { get; set; }
    }
}
