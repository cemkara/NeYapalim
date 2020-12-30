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
    public class UsersController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        [ResponseType(typeof(Users))]
        [HttpPost]
        public Users Login(Users user)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            return db.Users.Where(x => x.IsActive && x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
        }

        [ResponseType(typeof(Users))]
        [HttpPost]
        public Users GoogleLogin(Users user)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            return db.Users.Where(x => x.IsActive && x.Email == user.Email).FirstOrDefault();
        }

        [ResponseType(typeof(Users))]
        [HttpPost]
        public IHttpActionResult Register(Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Generic generic = new Generic();
            if (generic.EmailControl(user.Email))
            {
                user.UserTypeId = 2;
                user.RecordDate = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                //mail işlemi yapılacak. Doğrulama yapılacak
                return Ok(user);
            }
            else
            {
                return BadRequest("Bu e-postayı kullanan bir kullanıcı zaten kayıtlı. Bir hata olduğunu düşünüyorsanız hemen bize yazın..");
            }
        }

        [ResponseType(typeof(Users))]
        [HttpPost]
        public IHttpActionResult GoogleRegister(Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Generic generic = new Generic();
            if (generic.EmailControl(user.Email))
            {
                user.Password = generic.GetRandomPassword();
                user.UserTypeId = 2;
                user.RecordDate = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                //mail işlemi yapılacak. Doğrulama yapılacak
                return Ok(user);
            }
            else
            {
                return BadRequest("Bu e-postayı kullanan bir kullanıcı zaten kayıtlı. Bir hata olduğunu düşünüyorsanız hemen bize yazın..");
            }
        }

        
        public Users GetUser(int id)
        {
            return db.Users.Where(x => x.Id == id && x.IsActive).FirstOrDefault();
        }

        /// <summary>
        /// List of user favorite places
        /// </summary>
        public IQueryable<Places> GetFavoritePlaces(int id)
        {
            return db.UserFavoritePlaces.Where(x => x.UserId == id && x.IsActive).Select(x => x.Places);
        }

        /// <summary>
        /// List of user visited places
        /// </summary>
        public IQueryable<Places> GetVisitedPlaces(int id)
        {
            return db.UserVisitedPlaces.Where(x => x.UserId == id).Select(x => x.Places);
        }

        /// <summary>
        /// List of user favorite properties
        /// </summary>
        public IQueryable<Properties> GetFavoriteProperties(int id)
        {
            return db.UserFavoriteProperties.Where(x => x.UserId == id && x.IsActive).Select(x => x.Properties);
        }

        /// <summary>
        /// List of user favorite products
        /// </summary>
        public IQueryable<AllProducts> GetFavoriteProducts(int id)
        {
            return db.UserFavoriteProducts.Where(x => x.UserId == id).Select(x => x.AllProducts);
        }

        /// <summary>
        /// List of user favorite comments 
        /// </summary>
        public IQueryable<Comments> GetComments(int id)
        {
            return db.Comments.Where(x => x.UserId == id && x.IsActive);
        }


        //actions
        [ResponseType(typeof(UserFavoritePlaces))]
        public IHttpActionResult ChangeFavoritePlaceStatus(UserFavoritePlaces userFavoritePlace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userFavoritePlace.RecordDate = DateTime.Now;
            userFavoritePlace.IsActive = true;
            db.UserFavoritePlaces.Add(userFavoritePlace);
            db.SaveChanges();

            return Ok();
        }

    }
}
