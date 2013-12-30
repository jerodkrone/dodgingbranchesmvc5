using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DodgingBranchesMVC5.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public string UserId { get; set; }
        public int RouteId { get; set; }
        public int? ParentCommentId { get; set; }
        public DateTime DateEntered { get; set; }
    }
}