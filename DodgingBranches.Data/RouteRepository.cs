using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;

namespace DodgingBranches.Data
{

    public class RouteContext : DbContext
    {
        public RouteContext() : base("DefaultConnection")
        {
        }

        public DbSet<Route> Routes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }

    public class RouteRepository : IRouteRepository
    {

        public void AddRoute(Models.Route route)
        {
            DodgingBranches.Data.Route myRoute = MapRouteModelToDb(route);

            using (var db = new RouteContext())
            {
                db.Routes.Add(myRoute);
                db.SaveChanges();
            }
        }

        private Route MapRouteModelToDb(Models.Route route)
        {
            var returnRoute = new Route();

            returnRoute.Name = route.Name;
            returnRoute.RouteId = route.RouteId;

            if (route.StartPoint != null)
            {
                returnRoute.StartPoint = DbGeography.FromText(string.Format("POINT({0} {1})", route.StartPoint.Longitude, route.StartPoint.Latitude));               
            }

            if (route.EndPoint != null)
            {
                returnRoute.EndPoint = DbGeography.FromText(string.Format("POINT({0} {1})", route.StartPoint.Longitude, route.StartPoint.Latitude));
            }

            returnRoute.UserId = route.UserId; 
            returnRoute.Comments = route.Comments.Select(x => new DodgingBranches.Data.Comment()
                {
                    CommentId = x.CommentId,
                    CommentText = x.CommentText,
                    ParentCommentId = x.ParentCommentId,
                    UserId = x.UserId,
                    DateEntered = x.DateEntered,
                    RouteId = x.RouteId
                }).ToList();

            returnRoute.DateEntered = route.DateEntered;
            returnRoute.Description = route.Description;

            if (route.StartLocation != null)
            {
                returnRoute.StartLocation = new Data.Address
                {
                    Address1 = route.StartLocation.Address1,
                    City = route.StartLocation.City,
                    AddressId = route.StartLocation.AddressId,
                    State = route.StartLocation.State,
                    ZipCode = route.StartLocation.ZipCode
                };
            }

            if (route.EndLocation != null)
            {
                returnRoute.EndLocation = new Data.Address
                {
                    Address1 = route.EndLocation.Address1,
                    City = route.EndLocation.City,
                    AddressId = route.EndLocation.AddressId,
                    State = route.EndLocation.State,
                    ZipCode = route.EndLocation.ZipCode
                };
            }

            returnRoute.Tags = route.Tags.Select(x => new Data.Tag
            {
                TagId = x.TagId,
                TagText = x.TagText
            }).ToList();

            return returnRoute;
        }


        private Models.Route MapDbToRouteModel(Route route)
        {
            var returnRoute = new Models.Route();

            returnRoute.Name = route.Name;
            returnRoute.RouteId = route.RouteId;

            if (route.StartPoint != null)
            {
                returnRoute.StartPoint = new Models.MapPoint {Latitude = route.StartPoint.Latitude.Value, Longitude = route.StartPoint.Longitude.Value  };
            }

            if (route.EndPoint != null)
            {
                returnRoute.EndPoint = new Models.MapPoint { Latitude = route.EndPoint.Latitude.Value, Longitude = route.EndPoint.Longitude.Value };
            }

            returnRoute.UserId = route.UserId;
            returnRoute.Comments = route.Comments.Select(x => new Models.Comment()
            {
                CommentId = x.CommentId,
                CommentText = x.CommentText,
                ParentCommentId = x.ParentCommentId,
                UserId = x.UserId,
                DateEntered = x.DateEntered,
                RouteId = x.RouteId
            }).ToList();

            returnRoute.DateEntered = route.DateEntered;
            returnRoute.Description = route.Description;

            if (route.StartLocation != null)
            {
                returnRoute.StartLocation = new Models.Address
                {
                    Address1 = route.StartLocation.Address1,
                    City = route.StartLocation.City,
                    AddressId = route.StartLocation.AddressId,
                    State = route.StartLocation.State,
                    ZipCode = route.StartLocation.ZipCode
                };
            }

            if (route.EndLocation != null)
            {
                returnRoute.EndLocation = new Models.Address
                {
                    Address1 = route.EndLocation.Address1,
                    City = route.EndLocation.City,
                    AddressId = route.EndLocation.AddressId,
                    State = route.EndLocation.State,
                    ZipCode = route.EndLocation.ZipCode
                };
            }

            returnRoute.Tags = route.Tags.Select(x => new Models.Tag
            {
                TagId = x.TagId,
                TagText = x.TagText
            }).ToList();

            return returnRoute;
        }


        public List<Models.Route> GetRoutesForLocation(int latitude, int longitude)
        {
            var returnList = new List<Models.Route>();
            var routesFromDb = new List<Route>();

            using (var db = new RouteContext())
            {
                routesFromDb = db.Routes.Select(x=>x).ToList();
                foreach (var route in routesFromDb)
                {
                    returnList.Add(MapDbToRouteModel(route));
                }
            }



            return returnList;
        }


        public Models.Route GetRoute(int id)
        {
            Models.Route returnRoute;

            using (var db = new RouteContext())
            {
                var routeFromDb = db.Routes.Find(id);
                returnRoute = MapDbToRouteModel(routeFromDb);
            }

            return returnRoute;
        }
    }
}


