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
    public class CategoriesController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        // POST: api/Categories/5
        public IQueryable<Categories> GetCategories(int id)
        {
            return db.Categories.Where(x => x.MainCategoryId == id && x.IsActive).OrderBy(x => x.OrderNo);
        }
    }
}