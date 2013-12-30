using System;
using System.Collections.Generic;
//using System.Data.Spatial;
using System.Linq;
using System.Web;

namespace DodgingBranchesMVC5.Models
{
    public class RouteViewModel : RouteViewModelBase
    {
        public AddressViewModel StartLocation { get; set; }
        public AddressViewModel EndLocation { get; set; }
    }
}