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

        // GET: api/Blogs
        public IQueryable<Blogs> GetBlogs()
        {
            return db.Blogs.Where(x=>x.IsActive).OrderByDescending(x=>x.RecordDate);
        }
    }
}