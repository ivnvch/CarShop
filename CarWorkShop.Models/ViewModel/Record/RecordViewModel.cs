using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Models.ViewModel.Record
{
    public class RecordViewModel
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public short Age { get; set; }

        public string DateTime { get; set; }
        public string? Complaint { get; set; } //жалобы 


        public string Mark { get; set; }
        public string Model { get; set; }
        public string CarNumber { get; set; }
    }
}
