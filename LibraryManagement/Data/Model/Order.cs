using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Model
{
    public enum eOrderStatus
    {
        NotStarted,
        InProgress,
        Completed,
        ErroredOut,
        Stopped
    }
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public virtual Catalog Catalog { get; set; }
        public int CatalogId { get; set; }
        public string JenkinsBuildNumber { get; set; }
        public string JenkinsProjectName { get; set; }
        public string JenkinsDetails { get; set; }
        public string JenkinsConsoleText { get; set; }

        public eOrderStatus Status { get; set; }

    }
}
