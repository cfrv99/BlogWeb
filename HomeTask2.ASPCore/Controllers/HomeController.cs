using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeTask2.ASPCore.Models;
using HomeTask2.ASPCore.Contexts;
using Microsoft.AspNetCore.Identity;
using HomeTask2.ASPCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HomeTask2.ASPCore.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationdataContext context;
        private UserManager<User> userManager;

        public HomeController(ApplicationdataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        int pageSize = 2;
        public async Task<IActionResult> Index(int page = 1, string q = "")
        {
            if (page <= 0)
            {
                return NotFound();
            }
            var TotalItems = context.Posts.Count();
            var entity = context.Posts.Include(i => i.User)
                .Include(i => i.Comments)
                .Select(i => new HomeViewModel
                {
                    Id = i.Id,
                    PostName = i.PostName,
                    PostTitle = i.PostTitle,
                    PostDescr = i.PostDescription,
                    ImageUrl = i.ImageUrl,
                    PostDate = i.PostDate,
                    PostUserName = i.User.UserName,
                    TotalPage = (int)Math.Ceiling((decimal)TotalItems / pageSize),
                    ShowingCount = i.ShowingCount,
                    CommentsCount = i.Comments.Count()


                }).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            if (!string.IsNullOrEmpty(q))
            {
                entity = null;
                entity = context.Posts.Include(i => i.User).Include(i => i.Comments).Where(i => i.PostName.Contains(q) || i.PostTitle.Contains(q) || i.PostDescription.Contains(q) || i.User.UserName.Contains(q))
                    .Select(i => new HomeViewModel
                    {
                        Id = i.Id,
                        PostName = i.PostName,
                        PostTitle = i.PostTitle,
                        PostDescr = i.PostDescription,
                        ImageUrl = i.ImageUrl,
                        PostDate = i.PostDate,
                        PostUserName = i.User.UserName,
                        TotalPage = (int)Math.Ceiling((decimal)TotalItems / pageSize),
                        ShowingCount = i.ShowingCount,
                        CommentsCount = i.Comments.Count()
                    }).ToList();
            }



            var posts = await context.Posts.ToListAsync();



            ViewBag.CurrentPage = page;

            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalItems / pageSize);


            return View(entity);
        }
        int counter = 0;
        static int PostID;
        public async Task<IActionResult> Details(int id)
        {
            PostID = id;
            TempData["postId"] = id;
            counter = counter + 1;
            var forCounter = await context.Posts.FirstOrDefaultAsync(i => i.Id == id);
            forCounter.ShowingCount++;
            await context.SaveChangesAsync();

            var comments = context.Comments.Include(i => i.User).Where(i => i.PostId == id).ToList();

            var comment = context.Comments.Include(i => i.User).Where(i => i.PostId == id)
                .Select(i => new ShowCommentViewModel
                {
                    CommentText = i.Text,
                    CommentUser = i.User.UserName,
                    Id = i.Id,
                    Date = i.CommentDate,
                    ImageUrl = i.User.ImageUrl

                }).ToList();

            var post = context.Posts.Include(i => i.User).Where(i => i.Id == id)
                 .Select(i => new DetailViewModel
                 {
                     Id = i.Id,
                     PostName = i.PostName,
                     PostTitle = i.PostTitle,
                     PostUser = i.User.UserName,
                     PostDate = i.PostDate,
                     ImageUrl = i.ImageUrl,
                     Comments = comments,
                     PostDescription = i.PostDescription,
                     ShowingCount = i.ShowingCount,
                     PostUserId = i.User.Id

                 }).FirstOrDefault();

            //ViewBag.Comments = context.Comments.Include(i=>i.User).Where(i => i.PostId == post.Id).ToList();
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                comment.CommentDate = DateTime.Now;
                comment.UserId = user.Id;

                context.Comments.Add(comment);
                await context.SaveChangesAsync();

                return RedirectToAction("Details", new { Id = comment.PostId });

            }

            return View(comment);
        }

        [HttpGet]
        public IActionResult Remove(int? id)
        {
            var c = context.Comments.Include(i=>i.User).FirstOrDefault(i => i.Id == id);

            if (User.Identity.Name == c.User.UserName)
            {
                int Id = Convert.ToInt32(TempData["postId"]);
                var comment = context.Comments.FirstOrDefault(i => i.Id == id);

                context.Comments.Remove(comment);
                context.SaveChanges();

                return RedirectToAction("Details", new { Id = PostID });

            }

            return View("Error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
