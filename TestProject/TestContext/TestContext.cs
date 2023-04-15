using Dal;
using Entities;
using System;
using System.Data.Entity;

namespace TestingDemo
{
    public class TestContext : DbContext, IBloggingContext
    {
        public IDbSet<Blog> Blogs { get; }
        public IDbSet<Post> Posts { get; }

        public TestContext()
        {
            this.Blogs = new TestBlogDbSet();
            this.Posts = new TestDbSet<Post>();
        }

        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            this.SaveChangesCount++;
            return 1;
        }

    }
}
