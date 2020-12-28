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
using Microsoft.Ajax.Utilities;
using StartAppModel;

namespace StartApp.Controllers
{
    public class UserFavoritePlacesController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        /// <summary>
        /// User add to favorite place
        /// </summary>
        /// <param name="userFavoritePlace"></param>
        /// <returns></returns>
        [ResponseType(typeof(UserFavoritePlaces))]
        public IHttpActionResult AddFavoritePlace(UserFavoritePlaces userFavoritePlace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (UserFavoritePlaceExists(userFavoritePlace.Id))
            {
                userFavoritePlace.IsActive = true;
                db.Entry(userFavoritePlace).State = EntityState.Modified;

            }
            else
            {
                userFavoritePlace.RecordDate = DateTime.Now;
                userFavoritePlace.IsActive = true;
                db.UserFavoritePlaces.Add(userFavoritePlace);
            }

            db.SaveChanges();
            return Ok(userFavoritePlace);
        }

        /// <summary>
        /// User deletes favorite place
        /// </summary>
        /// <param name="userFavoritePlace"></param>
        /// <returns></returns>
        [ResponseType(typeof(UserFavoritePlaces))]
        public IHttpActionResult RemoveFavoritePlace(UserFavoritePlaces userFavoritePlace)
        {
            userFavoritePlace = db.UserFavoritePlaces.Where(x => x.PlaceId == userFavoritePlace.PlaceId && x.UserId == userFavoritePlace.UserId && x.IsActive).OrderByDescending(x => x.RecordDate).FirstOrDefault();
            if (userFavoritePlace != null)
            {
                userFavoritePlace.IsActive = false;
                db.Entry(userFavoritePlace).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(userFavoritePlace);
            }
            else
            {
                return BadRequest();
            }
        }

        [ResponseType(typeof(UserFavoritePlaces))]
        public bool UserFavoritePlaceControl(UserFavoritePlaces userFavoritePlace)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            if (db.UserFavoritePlaces.Where(x => x.PlaceId == userFavoritePlace.PlaceId && x.UserId == userFavoritePlace.UserId && x.IsActive).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //private
        private bool UserFavoritePlaceExists(int id)
        {
            return db.UserFavoritePlaces.Count(e => e.Id == id) > 0;
        }

    }
}