using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Model
{
    public class JenkinPipeline
    {
        public int JenkinPipelineId { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Color { get; set; }

    }
}
