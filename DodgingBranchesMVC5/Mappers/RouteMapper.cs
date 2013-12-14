using DodgingBranchesMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using DodgingBranches.Models;

namespace DodgingBranchesMVC5.Mappers
{
    public interface IRouteMapper
    {
        EditRouteViewModel MapEditDomainToModel(EditRouteViewModel model);
        EditRouteViewModel MapEditModelToDomain(EditRouteViewModel model,IPrincipal user);

        RoutesViewModel MapDomainToModel();
    }

    public class RouteMapper : IRouteMapper
    {

        public EditRouteViewModel MapEditDomainToModel(EditRouteViewModel model)
        {
            
            model = model ?? new EditRouteViewModel();

            MapEditLists(model);
            return model;
        }

        public EditRouteViewModel MapEditModelToDomain(EditRouteViewModel model, IPrincipal user)
        {
            model.EnteredBy = new ApplicationUser { 
                UserName = user.Identity.Name
            };
           
            return model;
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


        public RoutesViewModel MapDomainToModel()
        {
            var model = new RoutesViewModel();

            model.Location = "Minneapolis, MN";
            model.Routes = new List<RouteViewModel>
            {
                new RouteViewModel
                {
                    Id=1,
                    Description = "Cool Route",
                    //EndLocation = null,
                    //StartLocation = null,
                    Name = "Cool Route",
                    Score = 10,
                    Addr = new AddressViewModel
                    {
                        Addr = "1 Test Street",
                        City = "Buffalo",
                        State = "MN",
                        Zip = 55313
                    },
                    Tags = new List<TagViewModel>{new TagViewModel{Id=1, TagName="3-5 Miles"}, new TagViewModel{Id=2, TagName="Hills"}},
                    DateEntered = new DateTime(2013,01,01),
                    EnteredBy = new ApplicationUser{UserName="TestUser2"}
                },
                new RouteViewModel
                {
                    Id=2,
                    Description = "Cool Route 2",
                    //EndLocation = null,
                    //StartLocation = null,
                    Name = "Cool Route 2",
                    Score = 10,
                    Addr = new AddressViewModel
                    {
                        Addr = "2 Test Street",
                        City = "SLP",
                        State = "MN",
                        Zip = 55416
                    },
                    Tags = new List<TagViewModel>{new TagViewModel{Id=3, TagName="5-7 Miles"}, new TagViewModel{Id=4, TagName="Flat"}},
                    DateEntered = new DateTime(2012,01,01),
                    EnteredBy = new ApplicationUser{UserName="TestUser1"}
                }
            };

            return model;
        }
    }
}