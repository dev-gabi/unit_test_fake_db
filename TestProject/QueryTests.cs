using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace TestingDemo
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void GetAllBlogs_orders_by_name()
        {
            var context = new TestContext();
            context.Blogs.Add(new Blog { Name = "BBB" });
            context.Blogs.Add(new Blog { Name = "ZZZ" });
            context.Blogs.Add(new Blog { Name = "AAA" });

            var service = new BlogService(context);
            var blogs = service.GetAllBlogs();

            Assert.AreEqual(3, blogs.Count);
            Assert.AreEqual("AAA", blogs[0].Name);
            Assert.AreEqual("BBB", blogs[1].Name);
            Assert.AreEqual("ZZZ", blogs[2].Name);
        }

        [TestMethod]
        public void DeleteBlog_ShouldCountZeroElements()
        {
            var context = new TestContext();
            var service = new BlogService(context);
            Blog blog = new Blog() { Name = "Blog" };
            context.Blogs.Add(blog);
            int id = context.Blogs.First().BlogId;
            service.DeleteBlog(id);
            int actualCount = context.Blogs.Count();

            Assert.AreEqual(0, actualCount);

        }
    }
}
