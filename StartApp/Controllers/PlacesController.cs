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
using System.Web.Http.ModelBinding.Binders;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json;
using StartAppModel;

namespace StartApp.Controllers
{
    public class PlacesController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        /// <summary>
        /// Returns venue details
        /// </summary>
        public Places GetPlace(int id)
        {
            return db.Places.Find(id);
        }

        /// <summary>
        /// Filter recommended venues by category and features
        /// </summary>
        [ResponseType(typeof(RecommendationsForPost))]
        [HttpPost]
        public DbSqlQuery<Places> GetPlacesofRecommendations(RecommendationsForPost postData)
        {
            string[] arrayCategories = postData.categories.Split(',');
            string[] arrayProperties = postData.properties.Split(',');

            if (!ModelState.IsValid)
            {
                return null;
            }

            string SqlQuery = "SELECT  DISTINCT TOP 5 (P.Id),P.*,NEWID() FROM Places AS P inner join PlaceCategories AS PC ON P.Id = PC.PlaceId inner join PlaceProperties AS PP ON P.Id = PP.PlaceId WHERE ";
            if (postData.districtId != null && postData.districtId != 0)
            {
                SqlQuery += string.Format("DistrictId = {0} AND ", postData.districtId);
            }

            SqlQuery += string.Format(" P.IsActive=1 ");
            int catId, propId;

            if (arrayCategories.Count() > 0)
            {
                if (!(arrayCategories.Count() == 1 && arrayCategories[0] == "0"))
                {
                    SqlQuery += " AND (";
                    foreach (string cat in arrayCategories)
                    {
                        if (int.TryParse(cat, out catId))
                        {
                            SqlQuery += string.Format("PC.CategoryId = {0} OR ", catId);
                        }
                    }
                    SqlQuery = SqlQuery.Substring(0, SqlQuery.Length - 4);
                    SqlQuery += " )";
                }
            }

            if (arrayProperties.Count() > 0)
            {
                if (!(arrayProperties.Count() == 1 && arrayProperties[0] == "0"))
                {
                    SqlQuery += " AND (";
                    foreach (string prop in arrayProperties)
                    {
                        if (int.TryParse(prop, out propId))
                        {
                            SqlQuery += string.Format("PP.PropertyId = {0} OR ", propId);
                        }
                    }
                    SqlQuery = SqlQuery.Substring(0, SqlQuery.Length - 4);
                    SqlQuery += " )";
                }
            }

            SqlQuery += " ORDER BY NEWID()";

            return db.Places.SqlQuery(SqlQuery);
        }

        /// <summary>
        /// Filter recommended venues by category and features counts
        /// </summary>
        [ResponseType(typeof(RecommendationsForPost))]
        [HttpPost]
        public int GetPlacesofRecommendationsCount(RecommendationsForPost postData)
        {
            string[] arrayCategories = postData.categories.Split(',');
            string[] arrayProperties = postData.properties.Split(',');

            if (!ModelState.IsValid)
            {
                return 0;
            }

            string SqlQuery = "SELECT COUNT(DISTINCT(P.Id)) FROM Places AS P inner join PlaceCategories AS PC ON P.Id = PC.PlaceId inner join PlaceProperties AS PP ON P.Id = PP.PlaceId WHERE ";
            if (postData.districtId == null)
            {
                return 0;
            }

            SqlQuery += string.Format("DistrictId = {0} AND P.IsActive=1 ", postData.districtId);
            int catId, propId;

            if (arrayCategories.Count() > 0)
            {
                if (!(arrayCategories.Count() == 1 && arrayCategories[0] == "0"))
                {
                    SqlQuery += " AND (";
                    foreach (string cat in arrayCategories)
                    {
                        if (int.TryParse(cat, out catId))
                        {
                            SqlQuery += string.Format("PC.CategoryId = {0} OR ", catId);
                        }
                    }
                    SqlQuery = SqlQuery.Substring(0, SqlQuery.Length - 4);
                    SqlQuery += " )";
                }
            }

            if (arrayProperties.Count() > 0)
            {
                if (!(arrayProperties.Count() == 1 && arrayProperties[0] == "0"))
                {
                    SqlQuery += " AND (";
                    foreach (string prop in arrayProperties)
                    {
                        if (int.TryParse(prop, out propId))
                        {
                            SqlQuery += string.Format("PP.PropertyId = {0} OR ", propId);
                        }
                    }
                    SqlQuery = SqlQuery.Substring(0, SqlQuery.Length - 4);
                    SqlQuery += " )";
                }
            }

            return db.Database.SqlQuery<int>(SqlQuery).First();
        }

        /// <summary>
        /// Lists the categories of place
        /// </summary>
        public IQueryable<Categories> GetCategories(int id)
        {
            return db.PlaceCategories.Where(x => x.PlaceId == id && x.IsActive).Select(x => x.Categories);
        }

        /// <summary>
        /// Lists the properties of place
        /// </summary>
        public IQueryable<Properties> GetProperties(int id)
        {
            return db.PlaceProperties.Where(x => x.PlaceId == id && x.IsActive).Select(x => x.Properties);
        }

        /// <summary>
        /// List the comments of place
        /// </summary>
        public IQueryable<Comments> GetComments(int id)
        {
            return db.Comments.Where(x => x.PlaceId == id && x.IsActive).OrderByDescending(x=>x.RecordDate);
        }
      
        public IQueryable<Comments> GetCommentsTopTen(int id)
        {
            return db.Comments.Where(x => x.PlaceId == id && x.IsActive).Take(10).OrderByDescending(x => x.RecordDate);
        }


        /// <summary>
        /// List the products of place
        /// </summary>
        public IQueryable<PlaceProducts> GetProductMain(int id)
        {
            return db.PlaceProducts.Where(x => x.PlaceId == id && x.IsActive).Take(3);
        }

        public IQueryable<PlaceProducts> GetProducts(int id)
        {
            return db.PlaceProducts.Where(x => x.PlaceId == id && x.IsActive);
        }

        public int GetAveragePoint(int id)
        {
            IQueryable<Comments> commentList = db.Comments.Where(x => x.PlaceId == id && x.IsActive);
            int totalPoint = commentList.Select(x => x.Point).Count();
            int totalPointNumber = commentList.Count();

            return totalPoint / totalPointNumber;
        }
        //private
        private IEnumerable<Places> GetPlaceByCategoryId(int categoryId)
        {
            return db.PlaceCategories.Where(x => x.CategoryId == categoryId).Select(x => x.Places);
        }
        private IEnumerable<Places> GetPlaceByPropertyId(int propertyId)
        {
            return db.PlaceProperties.Where(x => x.PropertyId == propertyId).Select(x => x.Places);
        }

        public class RecommendationsForPost
        {
            public string categories { get; set; }
            public string properties { get; set; }
            public int? districtId { get; set; }
            public int? cityId { get; set; }
        }

    }
}