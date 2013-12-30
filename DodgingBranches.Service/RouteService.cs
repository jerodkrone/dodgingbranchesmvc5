using DodgingBranches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DodgingBranches.Data;

namespace DodgingBranches.Service
{
    public interface IRouteService
    {
        List<DodgingBranches.Models.Route> GetDefaultRoutesForUser(string userId);
        void AddRoute(DodgingBranches.Models.Route route);
        Models.Route GetRoute(int id);

    }
    public class RouteService : IRouteService
    {
        IRouteRepository _routeRepo;
        public RouteService(IRouteRepository routeRepo)
        {
            _routeRepo = routeRepo;
        }

        public List<DodgingBranches.Models.Route> GetDefaultRoutesForUser(string userId)
        {
            return _routeRepo.GetRoutesForLocation(0, 0);
        }


        public void AddRoute(Models.Route route)
        {
            _routeRepo.AddRoute(route);
        }

        public DodgingBranches.Models.Route GetRoute(int id)
        {
            DodgingBranches.Models.Route returnRoute = new Models.Route();

            returnRoute = _routeRepo.GetRoute(id);

            return returnRoute;
        }
    }
}
