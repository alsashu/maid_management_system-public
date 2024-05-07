using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaidManagementSolutions.ViewModel
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MaidId { get; set; }
        public Guid ClientId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }
        public string ReviewedBy { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
