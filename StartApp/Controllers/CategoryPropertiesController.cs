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
    public class CategoryPropertiesController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        public IQueryable<Properties> GetPropertiesOfCategories(string id)
        {
            string[] selectedCategories = id.Split(',');
            //List<CategoryProperties> properties = new List<CategoryProperties>();
            List<Properties> properties = new List<Properties>();
            int catId;
            foreach (string cat in selectedCategories)
            {
                if (Int32.TryParse(cat, out catId))
                {
                    properties.AddRange(db.CategoryProperties.Where(x => x.CategoryId == catId && x.IsActive).Select(x=>x.Properties));
                }
            }

            //return properties.Distinct().AsQueryable();
            return properties.Where(x=>x.IsActive).Distinct().AsQueryable();

            //        var result = EFContext.TestAddresses
            //.GroupBy(ta => ta.Name)
            //.Select(ta => ta.Key);
            //IQueryable<CategoryProperties> props = properties.GroupBy(x => x.CategoryId).Select(x => x.First()).AsQueryable();
            //if (props.Count() > 0)
            //{
            //    return props;
            //}
            //else
            //{
            //    return null;
            //}
        }
    }
}