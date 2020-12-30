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
    public class UserVisitedPlacesController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        [ResponseType(typeof(UserVisitedPlaces))]
        public IHttpActionResult AddUserVisitedPlace(UserVisitedPlaces userVisitedPlace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userVisitedPlace.RecordDate = DateTime.Now;
            db.UserVisitedPlaces.Add(userVisitedPlace);
            db.SaveChanges();
            return Ok(userVisitedPlace);
        }

        [ResponseType(typeof(UserFavoritePlaces))]
        public bool UserVisitedPlaceControl(UserVisitedPlaces userVisitedPlaces)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            if (db.UserVisitedPlaces.Where(x => x.PlaceId == userVisitedPlaces.PlaceId && x.UserId == userVisitedPlaces.UserId).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}