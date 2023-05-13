using Castle.Core.Resource;
using Dal;
using Entities;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using TestingDemo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestProjectWithMock
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void GetAllBlogs_orders_by_name()
        {
            var data = new List<Blog>
            {
                new Blog { Name = "AAA" },
                new Blog { Name = "BBB" },
                new Blog { Name = "ZZZ" }
            };
            var context = GetMockContext(data);
            BlogService service = new BlogService(context.Object);

            var blogs = service.GetAllBlogs();

            Assert.AreEqual(3, blogs.Count);
            Assert.AreEqual("AAA", blogs[0].Name);
            Assert.AreEqual("BBB", blogs[1].Name);
            Assert.AreEqual("ZZZ", blogs[2].Name);
        }

        [TestMethod]
        public void DeleteBlog_ShouldCountZeroElements()
        { 
            int blogId = 1;
            var data = new List<Blog>
            {
                new Blog { Name = "AAA" , BlogId = blogId},
            };

            var context = GetMockContext(data);
            BlogService service = new BlogService(context.Object);         
            service.DeleteBlog(blogId);

            int actualCount = service.GetAllBlogs().Count;      
            int expected = 0;

            Assert.AreEqual(expected, actualCount);
        }

        [TestMethod]
        public void AddBlog_ShouldCountOneElement()
        {
            var data = new List<Blog>{};
            var context = GetMockContext(data);
            BlogService service = new BlogService(context.Object);

            service.AddBlog("Cool Testing Unit", "some-url.com");
            int actual = service.GetAllBlogs().Count;
            int expected = 1;

            Assert.AreEqual(expected, actual);
        }
        private Mock<IBloggingContext> GetMockContext(List<Blog> blogs)
        {
            var mockSet = new Mock<DbSet<Blog>>();
            mockSet.Setup(b => b.Find(It.IsAny<object[]>())).Returns((object[] ids) =>
            {
                // Implement your custom Find logic here, for example:
                var id = (int)ids[0];
                return blogs.FirstOrDefault(b => b.BlogId == id);
            });

                mockSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogs.AsQueryable().Provider);
                mockSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogs.AsQueryable().Expression);
                mockSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogs.AsQueryable().ElementType);
                mockSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(() => blogs.GetEnumerator());
                mockSet.Setup(m => m.Remove(It.IsAny<Blog>())).Callback((Blog b) => blogs.Remove(b));
                mockSet.Setup(m=> m.Add(It.IsAny<Blog>())).Returns((Blog b)=>b).Callback((Blog b)=> blogs.Add(b));

            var mockContext = new Mock<IBloggingContext>();
            mockContext.Setup(c => c.Blogs).Returns(mockSet.Object);
            return mockContext;
        }

    }
}