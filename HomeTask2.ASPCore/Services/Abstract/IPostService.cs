using HomeTask2.ASPCore.Data;
using HomeTask2.ASPCore.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.Services.Abstract
{
    public interface IPostService:IPostRepository
    {
        List<Post> GetPosts();
        Post GetPost(int id);

        
    }
}
