using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using StartAppModel;

namespace StartApp.Controllers
{
    public class BlogsController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        public Blogs GetBlog(int id)
        {
            return db.Blogs.Find(id);
        }

        public IQueryable<Blogs> GetMainBlogs()
        {
            return db.Blogs.Where(x => x.IsActive).OrderByDescending(x => x.RecordDate).Take(6);
        }
        public IQueryable<Blogs> GetAllBlogs()
        {
            return db.Blogs.Where(x=>x.IsActive).OrderByDescending(x=>x.RecordDate);
        }
    }
}