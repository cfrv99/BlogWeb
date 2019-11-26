using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.Models
{
    public class PostEditViewModel
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string Description { get; set; }
        public string PostName { get; set; }
        public string ImageUrl { get; set; }
        public string ExistImage { get; set; }
    }
}
