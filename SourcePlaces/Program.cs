using StartAppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourcePlaces
{
    class Program
    {
        static int totalAddedPlace = 0;
        static void Main(string[] args)
        {
            //StartAppEntities db = new StartAppEntities();

            //string v1 = "Beşiktaş Merkez";

            //Districts ds =  db.Districts.Where(x => v1.ToLower().Contains(x.Name.ToLower())).FirstOrDefault();

            //Console.WriteLine(ds.Id);
            //Console.ReadLine();

            //ShowRestaurants("59", "0", "5");
            //Console.WriteLine(RestaurantCount());
            //Console.Read();

            //*****zincir mağazalar için isim kontrolünü kaldırmak lazım
            #region 
            List<string> strList = new List<string>();
            //strList.Add("köfte");
            //strList.Add("döner");
            //strList.Add("kahve");
            //strList.Add("iskender");
            //strList.Add("kebap");
            //strList.Add("starbucks");//zincir
            //strList.Add("kahve dünyası");//zincir
            strList.Add("künefe");


            foreach (string s in strList)
            {
                Console.WriteLine(s + ": Start.");
                searchLoop(s);
                Console.WriteLine(s + ": end.");

            }
            Console.WriteLine("Toplam " + totalAddedPlace + " mekan eklendi");
            Console.ReadLine();
            #endregion
        }

        public static void searchLoop(string q)
        {
            int start = 0;
            int count = 20;
            for (int i = 0; i < 20; i++)
            {
                start += count;
                SearchRestaurants("59", start.ToString(), count.ToString(), q);

            }
        }
        /// <summary>
        /// tek seferde hepsini çekmek istedik ama max 100 mekan veriyormuş
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private static void ShowRestaurants(string cityId, string start, string count)
        {
            var response = ZomatoInfo.GetRestaurants(cityId, start, count);
            DatabaseSave(response);
        }

        private static void SearchRestaurants(string cityId, string start, string count, string name)
        {
            var response = ZomatoInfo.SearchRestaurants(cityId, start, count, name);
            DatabaseSave(response);
        }

        private static int RestaurantCount()
        {
            var response = ZomatoInfo.GetRestaurants("59", "0", "10");

            return Convert.ToInt32(response.ResultsFound);

        }

        private static void DatabaseSave(RestaurantList list)
        {
            StartAppEntities db = new StartAppEntities();

            var restaurants = list.Restaurants;
            Places place;
            foreach (var r in restaurants)
            {
                var restaurant = r.Restaurant;
                place = new Places();

                //if(true)
                if (!db.Places.Any(x => x.Name.ToLower() == restaurant.Name.ToLower()))
                {
                    place.Name = restaurant.Name;

                    var districtList = db.Districts.Where(x => x.IsActive && restaurant.Location.Locality.ToLower().Contains(x.Name.ToLower()));
                    if (districtList.Count() > 0)
                    {
                        place.DistrictId = districtList.FirstOrDefault().Id;
                    }
                    else
                    {
                        Districts nD = new Districts();
                        nD.CityId = 1;
                        nD.Name = restaurant.Location.Locality;
                        nD.IsActive = true;
                        db.Districts.Add(nD);
                        db.SaveChanges();
                        place.DistrictId = nD.Id;
                    }

                    place.AveragePrice = restaurant.AverageCostForTwo;
                    place.Address = restaurant.Location.Address;
                    place.PlaceTypeID = 1;
                    place.Phone = restaurant.Phone.Split(',').First();
                    place.ShortDescription = "";
                    place.Description = "";
                    place.Latitude = double.Parse(restaurant.Location.Latitude);
                    place.Longitude = double.Parse(restaurant.Location.Longitude);
                    place.RecordDate = DateTime.Now;
                    place.IsActive = true;
                    db.Places.Add(place);
                    db.SaveChanges();
                    totalAddedPlace++;
                }
            }
        }
    }
}
