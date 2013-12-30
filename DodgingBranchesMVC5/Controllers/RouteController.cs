using DodgingBranchesMVC5.Mappers;
using DodgingBranchesMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using DodgingBranches.Service;

namespace DodgingBranchesMVC5.Controllers
{
    public class RouteController : Controller
    {

        private readonly IRouteMapper _mapper; 

        public RouteController(IRouteMapper mapper, IRouteService routeService)
        {
            _mapper = mapper;
        }

        public RouteController()
        {
        }

        //
        // GET: /Route/
        public ActionResult Index()
        {
            var model = new RoutesViewModel();

            model = _mapper.MapDomainToModel(User);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new EditRouteViewModel();

            model = _mapper.MapEditDomainToModel(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditRouteViewModel model)
        {
            if (ModelState.IsValid)
            {

                _mapper.MapEditModelToDomain(model, User);
                return RedirectToRoute(new {controller= "Route",Action="Index"});
            }

            return RedirectToAction("Create");
        }

        public ActionResult RouteDetails(int id)
        {
            var model = _mapper.MapDetailsDomainToViewModel(id);

            return View(model);
        }
	}
}