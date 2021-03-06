﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgingBranches.Models
{
    public class Route
    {
        public Route()
        {
            StartLocation = new Address();
            EndLocation = new Address();
            StartPoint = new MapPoint();
            EndPoint = new MapPoint();
        }

        public int RouteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Address StartLocation { get; set; }
        public Address EndLocation { get; set; }
        public MapPoint StartPoint { get; set; }
        public MapPoint EndPoint { get; set; }
        public DateTime DateEntered { get; set; }
        public string UserId { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Tag> Tags { get; set; }

    }

    public class Address
    {
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }

    public class MapPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Comment
    {
        public int CommentId { get; set; }
        public String CommentText { get; set; }
        public int RouteId { get; set; }
        public string UserId { get; set; }
        public DateTime DateEntered { get; set; }
        public int? ParentCommentId { get; set; }
    }

    public class Tag
    {
        public int TagId { get; set; }
        public string TagText { get; set; }
    }
}
