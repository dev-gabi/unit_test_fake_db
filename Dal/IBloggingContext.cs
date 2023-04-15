using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface IBloggingContext 
    {
         IDbSet<Blog> Blogs { get; }
         IDbSet<Post> Posts { get; }

         int SaveChanges();
    }
}
