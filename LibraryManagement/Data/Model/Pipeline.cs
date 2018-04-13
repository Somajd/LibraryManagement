using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Model
{
    public enum ePipelineEnabled
    {
        Yes,
        No

    }
    public class Pipeline
    {
        public int PipelineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Catalog Catalog { get; set;}
        public int CatalogId { get; set; }

        public string JenkinsWebAPI { get; set; }
        public string JenkinsPipelineName { get; set; }
        public string ServiceNowWebAPI { get; set; }

        public ePipelineEnabled Enabled { get; set; }
        
    }
}
