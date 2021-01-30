using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using StartAppModel;

namespace StartApp.Controllers
{
    public class DistrictsController : ApiController
    {
        private StartAppEntities db = new StartAppEntities();

        [HttpGet]
        public List<DistrictSearch> GetDistrict()
        {
            var list = db.Districts.AsEnumerable()
                          .Where(x => x.IsActive)
                          .Select(o => new DistrictSearch
                          {
                              id = o.Id,
                              name = o.Name + ", " + o.Cities.Name
                          }).ToList();
            list.Insert(0, new DistrictSearch { id = 0, name = "Bana hiç farketmez.." });
            return list;
        }

        [ResponseType(typeof(DistrictRequest))]
        public DistrictResponse SearchDistricts([FromBody]DistrictRequest districtRequest)
        {
            Districts dist = new Districts();
            DistrictResponse response = new DistrictResponse();

            if (!ModelState.IsValid)
            {
                return null;
            }

            dist = db.Districts.Where(x => x.Name.ToLower().Contains(districtRequest.District.ToLower())
                && x.Cities.Name.ToLower().Contains(districtRequest.City.ToLower())).FirstOrDefault();

            if (dist != null)
            {
                response.district = dist;
                response.isDistrict = true;
                response.city = dist.Cities;
            }
            else
            {
                Cities city = db.Cities.Where(x => x.Name.ToLower().Contains(districtRequest.City.ToLower())).FirstOrDefault();
                if (city != null)
                {
                    response.city = city;
                    response.isDistrict = false;
                }
                else
                {
                    return null;
                }
            }
            return response;
        }

        public class DistrictRequest
        {
            public string District { get; set; }
            public string City { get; set; }
        }
        public class DistrictResponse
        {
            public Districts district { get; set; }
            public Cities city { get; set; }
            public bool isDistrict { get; set; }
        }

        public class DistrictSearch
        {
            public int id { get; set; }
            public string name { get; set; }
        }

    }
}