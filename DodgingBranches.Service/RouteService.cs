using DodgingBranches.Data;
using DodgingBranches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DodgingBranches.Service
{
    public interface IRouteService
    {
        List<Route> GetRoutesForUser(ApplicationUser user);

    }
    public class RouteService : IRouteService
    {
        public RouteService(IRouteRepository _routeRepo)
        {

        }

        public List<Route> GetRoutesForUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
