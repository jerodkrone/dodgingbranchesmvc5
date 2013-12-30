using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DodgingBranchesMVC5.Models
{
    public class AddressEntryViewModel
    {

        public string Addr { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public List<SelectListItem> State { get; set; }
        public string SelectedState { get; set; }
        public int AddressId { get; set; }
    }

}