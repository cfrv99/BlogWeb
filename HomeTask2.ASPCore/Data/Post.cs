using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string PostName { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PostDate { get; set; }
        public int ShowingCount { get; set; }
        public List<Comment> Comments { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        
    }
}
