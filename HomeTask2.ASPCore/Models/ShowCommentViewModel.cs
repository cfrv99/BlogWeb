using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.Models
{
    public class ShowCommentViewModel
    {
        public int Id { get; set; }
        public string CommentUser { get; set; }
        public string CommentText { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
    }
}
