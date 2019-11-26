using HomeTask2.ASPCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.ViewComponents
{
    public class UserProfileViewComponent:ViewComponent
    {
        private UserManager<User> userManager;
        public UserProfileViewComponent(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
    }
}
