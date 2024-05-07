using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaidManagementSolutions.ViewModel
{
    public class ReportProfileViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MaidId { get; set; }
        public Guid ClientId { get; set; }
        public string ReportingComment { get; set; }=string.Empty;
        public DateTime ReportedOn { get; set; }
    }
}
