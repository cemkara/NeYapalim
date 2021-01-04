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
using System.Web.UI.WebControls;
using StartAppModel;

namespace StartApp.Controllers
{
    public class ContentsController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        // GET: api/Contents
        public IQueryable<Contents> GetMainContents()
        {
            return db.Contents.Where(x => x.IsActive && x.MainPage == true).OrderBy(x => x.OrderBy).Take(6);
        }

        public IQueryable<Contents> GetAllContent()
        {
            return db.Contents.Where(x => x.IsActive).OrderBy(x => x.OrderBy);
        }
    }
}