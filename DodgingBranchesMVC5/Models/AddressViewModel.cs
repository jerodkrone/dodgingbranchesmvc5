using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DodgingBranchesMVC5.Models
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string State { get; set; }
    }
}