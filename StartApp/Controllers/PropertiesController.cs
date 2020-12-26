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
    public class PropertiesController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: api/Properties
        public IQueryable<Properties> GetProperties()
        {
            return db.Properties.Where(x => x.IsActive).OrderBy(x => x.OrderNo);
        }
    }
}