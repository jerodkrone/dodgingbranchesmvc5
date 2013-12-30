using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using DodgingBranches.Service;
using DodgingBranchesMVC5.Models;
using DodgingBranches.Models;
using System.Collections.Generic;

namespace DodgingBranches.Tests.Mappers
{
    [TestClass]
    public class RouteMapperTest
    {
        private List<DodgingBranches.Models.Route> _returnRoutes;

        public RouteMapperTest()
        {
            _returnRoutes = new List<DodgingBranches.Models.Route>
            {
                new Route
                {
                    RouteId=1,
                    Description = "Cool Route",
                    //EndLocation = null,
                    //StartLocation = null,
                    Name = "Cool Route",
                    StartLocation = new Address
                    {
                        Address1 = "1 Test Street",
                        City = "Buffalo",
                        State = "MN",
                        ZipCode  = 55313
                    },
                    Tags = new List<Tag>{new Tag{TagId=1, TagText="3-5 Miles"}, new Tag{TagId=2, TagText="Hills"}},
                    DateEntered = new DateTime(2013,01,01),
                    UserId = "TestUser2",
                    StartPoint = new MapPoint {Longitude = 100, Latitude=150.11},
                    EndPoint = new MapPoint {Longitude = 1150, Latitude=1500.11},
                    Comments = new List<Models.Comment>()
                },
                new Route
                {
                    RouteId=2,
                    Description = "Cool Route 2",
                    //EndLocation = null,
                    //StartLocation = null,
                    Name = "Cool Route 2",
                    StartLocation = new Address
                    {
                        Address1 = "2 Test Street",
                        City = "SLP",
                        State = "MN",
                        ZipCode = 55416
                    },
                    Tags = new List<Tag>{new Tag{TagId=3, TagText="5-7 Miles"}, new Tag{TagId=4, TagText="Flat"}},
                    DateEntered = new DateTime(2012,01,01),
                    UserId = "TestUser1",
                    StartPoint = new MapPoint {Longitude = 100, Latitude=150.11},
                    EndPoint = new MapPoint {Longitude = 1150, Latitude=1500.11},
                    Comments = new List<Models.Comment>()
                }
            };
        }

        [TestMethod]
        public void TestGetDefaultRoutesForUser()
        {
            //Arrange
            IRouteService routeServiceMock = MockRepository.GenerateMock<IRouteService>();
            routeServiceMock.Expect(x => x.GetDefaultRoutesForUser("TestUser")).Return(_returnRoutes);

            var mapperUnderTest = new DodgingBranchesMVC5.Mappers.RouteMapper(routeServiceMock);
            
            System.Security.Principal.IPrincipal pricipalMock = MockRepository.GenerateMock<System.Security.Principal.IPrincipal>();
            pricipalMock.Expect(x => x.Identity.Name).Return("TestUser");

            //Act 
            var returnedFromText = mapperUnderTest.MapDomainToModel(pricipalMock);

            //Assert
            Assert.AreEqual(2, returnedFromText.Routes.Count);
            Assert.AreEqual("1 Test Street", returnedFromText.Routes[0].StartLocation.AddressLine1);
            Assert.AreEqual("2 Test Street", returnedFromText.Routes[1].StartLocation.AddressLine1);
            Assert.AreEqual(2, returnedFromText.Routes[0].Tags.Count);
            Assert.AreEqual(2, returnedFromText.Routes[1].Tags.Count);
        }
    }
}
