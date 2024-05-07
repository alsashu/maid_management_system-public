using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaidManagementSolutions.ViewModel
{
    public class MaidOptionTwoViewModel
    {
        public string Id { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime AvailableFrom { get; set; }
        public string Location { get; set; }
        public string Community { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string About { get; set; }
        public string[] PreferredLocation { get; set; }
        //public string Address { get; set; }
    }
}
