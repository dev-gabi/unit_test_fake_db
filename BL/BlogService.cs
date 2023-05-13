using Dal;
using Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
    using System.Threading.Tasks;

    namespace TestingDemo
    {
        public class BlogService
        {
            private IBloggingContext _context;

            public BlogService(IBloggingContext context)
            {
                _context = context;
            }

            public Blog AddBlog(string name, string url)
            {
                var blog = new Blog { Name = name, Url = url };
                _context.Blogs.Add(blog);
                _context.SaveChanges();

                return blog;
            }
            
            public void DeleteBlog(int id)
            {
              var blogToDelete = _context.Blogs.Find(id) ;

            _context.Blogs.Remove(blogToDelete);
               _context.SaveChanges();
        }
            public List<Blog> GetAllBlogs()
            {
                var query = from b in _context.Blogs
                            orderby b.Name
                            select b;
                return query.ToList();          
            }

            public async Task<List<Blog>> GetAllBlogsAsync()
            {
                //var query = from b in _context.Blogs
                //            orderby b.Name
                //            select b;
            var query = _context.Blogs.OrderBy(b => b.Name);
            return await query.ToListAsync();
            }
        }
    }
