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
using Newtonsoft.Json;
using StartAppModel;

namespace StartApp.Controllers
{
    public class MainCategoriesController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: api/MainCategories
        public IQueryable<MainCategories> GetMainCategories()
        {
            return db.MainCategories.Where(x=>x.IsActive).OrderBy(x=>x.OrderNo);
        }
    }
}