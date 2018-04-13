using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Model
{
    public class OrderLog
    {
        public int OrderLogId { get; set; }
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
        public string ShortDescription { get; set; }
        public string LongDesciption { get; set; }
        public DateTime UpdatedData { get; set; }
        public string UpdatedBy { get; set; }

    }
}
