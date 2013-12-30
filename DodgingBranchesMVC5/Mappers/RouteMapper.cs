using DodgingBranchesMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using DodgingBranches.Models;
using DodgingBranches.Service;

namespace DodgingBranchesMVC5.Mappers
{
    public interface IRouteMapper
    {
        EditRouteViewModel MapEditDomainToModel(EditRouteViewModel model);
        EditRouteViewModel MapEditModelToDomain(EditRouteViewModel model,IPrincipal user);

        RouteViewModel MapDetailsDomainToViewModel(int id);

        RoutesViewModel MapDomainToModel(IPrincipal user);
    }

    public class RouteMapper : IRouteMapper
    {

        IRouteService _routeService;
        public RouteMapper(IRouteService routeService)
        {
            _routeService = routeService;
        }

        public EditRouteViewModel MapEditDomainToModel(EditRouteViewModel model)
        {
            
            model = model ?? new EditRouteViewModel();

            MapEditLists(model);
            return model;
        }

        public EditRouteViewModel MapEditModelToDomain(EditRouteViewModel model, IPrincipal user)
        {


            model.DateEntered = DateTime.Now;

            DodgingBranches.Models.Route routeToAdd = MapViewModelToDomain(model);

            _routeService.AddRoute(routeToAdd);
           
            return model;
        }

        private DodgingBranches.Models.Route MapViewModelToDomain(Models.EditRouteViewModel routeViewModel)
        {
            var returnRoute = new DodgingBranches.Models.Route();

            returnRoute.Name = routeViewModel.Name;
            returnRoute.RouteId = routeViewModel.Id;
            returnRoute.StartPoint = new MapPoint { Latitude = routeViewModel.StartPoint.Latitude, Longitude = routeViewModel.StartPoint.Longitude };
            returnRoute.EndPoint = new MapPoint { Latitude = routeViewModel.EndPoint.Latitude, Longitude = routeViewModel.EndPoint.Longitude };
            returnRoute.UserId = routeViewModel.EnteredBy;

            returnRoute.Comments = routeViewModel.Comments.Select(x=> new DodgingBranches.Models.Comment
                {
                    CommentId = x.Id,
                    CommentText = x.CommentText,
                    DateEntered = x.DateEntered,
                    ParentCommentId = x.ParentCommentId,
                    RouteId = x.RouteId,
                    UserId = x.UserId
                }).ToList();

            returnRoute.DateEntered = routeViewModel.DateEntered;
            returnRoute.Description = routeViewModel.Description;
            returnRoute.StartLocation = new DodgingBranches.Models.Address
            {
                Address1 = routeViewModel.Addr.Addr,
                City = routeViewModel.Addr.City,
                AddressId = routeViewModel.Addr.AddressId,
                State = routeViewModel.Addr.SelectedState,
                ZipCode = routeViewModel.Addr.Zip
            };

            returnRoute.EndLocation = new DodgingBranches.Models.Address
            {
                Address1 = routeViewModel.EndAddr.Addr,
                City = routeViewModel.EndAddr.City,
                AddressId = routeViewModel.EndAddr.AddressId,
                State = routeViewModel.EndAddr.SelectedState,
                ZipCode = routeViewModel.EndAddr.Zip
            };

            returnRoute.Tags = routeViewModel.Tags.Select(x => new DodgingBranches.Models.Tag
            {
                TagId = x.Id,
                TagText = x.TagName
            }).ToList();

            return returnRoute;
        }

        private void MapEditLists(EditRouteViewModel model)
        {
            var stateItems = new List<SelectListItem>{
                new SelectListItem{Text = "Minnesota", Value="MN"},
                new SelectListItem{Text = "Iowa", Value="IA"},
                new SelectListItem{Text = "Kansas", Value="KS"},
                new SelectListItem{Text = "North Dakota", Value="ND"}
            };

            model.Addr.State = stateItems;

        }


        public RoutesViewModel MapDomainToModel(IPrincipal user)
        {
            var model = new RoutesViewModel();

            model.Location = "Minneapolis, MN";

            var routes = _routeService.GetDefaultRoutesForUser(user.Identity.Name);

            return BuildRoutesViewModel(routes);
        }

        private RoutesViewModel BuildRoutesViewModel(List<Route> routes)
        {
            var returnViewModel = new RoutesViewModel();

            returnViewModel.Routes = new List<RouteViewModel>();

            foreach (var route in routes)
            {
                returnViewModel.Routes.Add(BuildRouteViewModelFromDomain(route));
            }

            return returnViewModel;
        }

        private RouteViewModel BuildRouteViewModelFromDomain(DodgingBranches.Models.Route route)
        {
            var returnRoute = new RouteViewModel();

            returnRoute.Name = route.Name;
            returnRoute.Id = route.RouteId;
            returnRoute.StartPoint = new RouteMapPoint { Latitude = route.StartPoint.Latitude, Longitude = route.StartPoint.Longitude } ;
            returnRoute.EndPoint = new RouteMapPoint { Latitude = route.EndPoint.Latitude, Longitude = route.EndPoint.Longitude };
            returnRoute.EnteredBy = route.UserId;

            if (route.Comments != null)
            {
                returnRoute.Comments = route.Comments.Select(x => new Models.Comment
                {
                    Id = x.CommentId,
                    CommentText = x.CommentText,
                    DateEntered = x.DateEntered,
                    ParentCommentId = x.ParentCommentId,
                    RouteId = x.RouteId,
                    UserId = x.UserId
                }).ToList();                
            }

            returnRoute.DateEntered = route.DateEntered;
            returnRoute.Description = route.Description;
            returnRoute.StartLocation = new Models.AddressViewModel
            {
                AddressLine1 = route.StartLocation.Address1,
                City = route.StartLocation.City,
                AddressId = route.StartLocation.AddressId,
                State = route.StartLocation.State,
                ZipCode = route.StartLocation.ZipCode
            };

            returnRoute.EndLocation = new Models.AddressViewModel
            {
                AddressLine1 = route.EndLocation.Address1,
                City = route.EndLocation.City,
                AddressId = route.EndLocation.AddressId,
                State = route.EndLocation.State,
                ZipCode = route.EndLocation.ZipCode
            };

            if (route.Tags != null)
            {
                returnRoute.Tags = route.Tags.Select(x => new Models.TagViewModel
                {
                    Id = x.TagId,
                    TagName = x.TagText
                }).ToList();                
            }


            return returnRoute;

        }

        public RouteViewModel MapDetailsDomainToViewModel(int id)
        {
            var returnViewModel = new RouteViewModel();

            var domainRoute = _routeService.GetRoute(id);

            returnViewModel = BuildRouteViewModelFromDomain(domainRoute);

            return returnViewModel;
        }
    }
}