using StartAppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace StartApp.Controllers
{
    public class CommentsController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        /// <summary>
        /// Add comment
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Comments))]
        public IHttpActionResult AddComment(Comments comments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comments.IsActive = true;
            comments.RecordDate = DateTime.Now;
            db.Comments.Add(comments);
            db.SaveChanges();
            return Ok(comments);
        }
    }
}
