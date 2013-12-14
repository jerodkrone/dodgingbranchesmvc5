using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DodgingBranchesMVC5.Models
{
    public class EditRouteViewModel : RouteViewModelBase
    {
        public EditRouteViewModel()
        {
            Addr = new AddressEntryViewModel();
        }

        public AddressEntryViewModel Addr { get; set; }
    }
}