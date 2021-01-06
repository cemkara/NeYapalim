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
        public Categories GetCategory(int id)
        {
            return db.Categories.Find(id);
        }
        public IQueryable<Categories> GetCategories(int id)
        {
            return db.Categories.Where(x => x.MainCategoryId == id && x.IsActive).OrderBy(x => x.OrderNo);
        }

        public IQueryable<Categories> GetMainCategories()
        {
            return db.Categories.Where(x => x.IsActive).OrderBy(x => x.OrderNo).Take(9);
        }

        public IQueryable<Categories> GetAllCategories()
        {
            return db.Categories.Where(x => x.IsActive).OrderBy(x => x.OrderNo);
        }

        public IQueryable<Places> GetPlacesByCategoryId(int id)
        {
            return db.PlaceCategories.Where(x => x.CategoryId == id && x.IsActive && x.Places.IsActive).Select(x => x.Places);
        }
    }
}