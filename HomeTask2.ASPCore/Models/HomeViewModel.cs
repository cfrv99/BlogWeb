using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.Models
{
    public class HomeViewModel
    {
        public int Id { get; set; }
        public string PostName { get; set; }
        public string PostTitle { get; set; }
        public string ImageUrl { get; set; }
        public string PostDescr { get; set; }
        public DateTime PostDate { get; set; }
        public string PostUserName { get; set; }
        public int ShowingCount { get; set; }
        public int TotalPage { get; set; }
        public int CommentsCount { get; set; }

    }
}
