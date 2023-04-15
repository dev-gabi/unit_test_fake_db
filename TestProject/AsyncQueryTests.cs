using Entities;

namespace TestingDemo
{
    [TestClass]
        public class AsyncQueryTests
        {
           [TestMethod]
            public async Task GetAllBlogsAsync_orders_by_name()
            {
                var context = new TestContext();
                context.Blogs.Add(new Blog { Name = "BBB" });
                context.Blogs.Add(new Blog { Name = "ZZZ" });
                context.Blogs.Add(new Blog { Name = "AAA" });

                var service = new BlogService(context);
                var blogs = await service.GetAllBlogsAsync();

                Assert.AreEqual(3, blogs.Count);
                Assert.AreEqual("AAA", blogs[0].Name);
                Assert.AreEqual("BBB", blogs[1].Name);
                Assert.AreEqual("ZZZ", blogs[2].Name);
            }
        }
    }

