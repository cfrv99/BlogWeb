using HomeTask2.ASPCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.Repository.Abstract
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Delete(Post post);
        void Update(Post post);
    }
}
