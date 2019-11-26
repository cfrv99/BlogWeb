using HomeTask2.ASPCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask2.ASPCore.Models;

namespace HomeTask2.ASPCore.Contexts
{
    public class ApplicationdataContext:IdentityDbContext<User,IdentityRole,string>
    {
        public ApplicationdataContext(DbContextOptions<ApplicationdataContext> options):base(options)
        {

        }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<HomeTask2.ASPCore.Models.PostEditViewModel> PostEditViewModel { get; set; }
        public DbSet<SliderImg> Sliders { get; set; }

    }
}
