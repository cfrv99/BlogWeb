using HomeTask2.ASPCore.Contexts;
using HomeTask2.ASPCore.Data;
using HomeTask2.ASPCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.Controllers
{
    public class ManagmentController : Controller
    {
        private ApplicationdataContext context;
        private UserManager<User> userManager;

        public ManagmentController(ApplicationdataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }


        [Authorize]
        public IActionResult Add()   //View hazirlamaq lazimdir 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPostVM model, IFormFile file)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);


            if (file != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                    model.ImageUrl = file.FileName;
                }

            }
            if (ModelState.IsValid)
            {
                Post p = new Post
                {
                    PostName = model.Name,
                    PostTitle = model.Title,
                    PostDescription = model.Description,
                    UserId = user.Id,

                    PostDate = DateTime.Now,
                    ImageUrl = model.ImageUrl
                };

                context.Posts.Add(p);
                await context.SaveChangesAsync();
                return Redirect("/");

            }

            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> UserPosts()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var usersInThis = context.Posts
                .Include(i => i.User)
                 .Where(i => i.UserId == user.Id).ToList();

            return View(usersInThis);
        }

        public IActionResult RelUserPost(string id)
        {
            var userPosts = context.Posts
                .Include(i => i.User)
                .Where(i => i.UserId == id)
                .ToList();
            return View(userPosts);
        }
        [Authorize]
        public IActionResult Edit(int? id)
        {

            var p = context.Posts.Include(i => i.User).FirstOrDefault(i => i.Id == id);
            if (User.Identity.Name == p.User.UserName)
            {
                var post = context.Posts
                .Include(i => i.User)
                .Include(i => i.Comments)
                .Where(i => i.Id == id)
                .Select(i => new PostEditViewModel
                {
                    PostName = i.PostName,
                    PostTitle = i.PostTitle,
                    Description = i.PostDescription,
                    ImageUrl = i.ImageUrl,
                    Id = i.Id
                }).FirstOrDefault();
                return View(post);
            }

            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public IActionResult Edit(PostEditViewModel model, IFormFile file)
        {
            var post = context.Posts.FirstOrDefault(i => i.Id == model.Id);

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (System.IO.File.Exists($"wwwroot\\img\\{model.ExistImage}"))
                    {
                        System.IO.File.Delete($"wwwroot\\img\\{model.ExistImage}");
                    }
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fs);
                        model.ImageUrl = file.FileName;
                    }

                }
                post.PostTitle = model.PostTitle;
                post.PostName = model.PostName;
                post.PostDescription = model.Description;
                post.ImageUrl = model.ImageUrl;
                post.PostDate = DateTime.Now;

                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
            //Sabah Dersde Yazacam!!!
        }

        public IActionResult AddSlider()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSlider(SliderViewModel model, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string fileName = Guid.NewGuid() + file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\sliderimg", fileName);
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fs);
                        model.ImageUrl = fileName;
                    }
                }

                SliderImg s = new SliderImg();
                s.ImageUrl = model.ImageUrl;
                s.ImgContent = model.SliderContent;
                context.Sliders.Add(s);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        private async Task<User> GetCurrentUser()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            return user;
        }
    }
}
