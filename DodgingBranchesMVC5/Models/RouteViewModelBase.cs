using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DodgingBranches.Models;

namespace DodgingBranchesMVC5.Models
{
    public class RouteViewModelBase
    {
        public RouteViewModelBase()
        {
            Comments = new List<Comment>();
            StartPoint = new RouteMapPoint();
            EndPoint = new RouteMapPoint();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public int Score { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public ApplicationUser EnteredBy { get; set; }
        public List<Comment> Comments { get; set; }
        public RouteMapPoint StartPoint { get; set; }
        public RouteMapPoint EndPoint { get; set; }
        public DateTime DateEntered { get; set; }
     }

    public class RoutesViewModel
    {
        public string Location { get; set; }
        public List<RouteViewModel> Routes { get; set; }
    }

    public class RouteMapPoint
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}