using LibraryManagement.Data.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModel
{
    public class CatalogViewModel
    {
        public Catalog Catalog { get; set; }
        public int PipelineCount { get; set; }

        public List<SelectListItem> PublishStatuses { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = ePublishedStatus.Yes.ToString(), Text = ePublishedStatus.Yes.ToString() },
            new SelectListItem { Value =  ePublishedStatus.No.ToString(), Text = ePublishedStatus.No.ToString() },
            
        };
    }
}
