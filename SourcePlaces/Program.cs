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
            StartAppEntities db = new StartAppEntities();

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
            //strList.Add("künefe");
            //strList.Add("beyaz");
            //strList.Add("siyah");
            //strList.Add("muhallebi");
            //strList.Add("dondurma");
            //strList.Add("pilav");
            //strList.Add("muhallebi");
            //strList.Add("taksim");
            //strList.Add("nevizade");
            //strList.Add("meyhane");
            //strList.Add("pastane");
            //strList.Add("bar");
            //strList.Add("pizza");
            //strList.Add("mantı");
            //strList.Add("noddle");
            //strList.Add("pizza");
            //strList.Add("burger");
            //strList.Add("simit");
            //strList.Add("balık");
            //eklenecekler


            //foreach (string s in strList)
            //{
            //    Console.WriteLine(s + ": Start.");
            //    searchLoop(s);
            //    Console.WriteLine(s + ": end.");

            //}
            //Console.WriteLine("Toplam " + totalAddedPlace + " mekan eklendi");
            //Console.ReadLine();
            #endregion
            List<Places> places = db.Places.Where(x => x.Id > 1002).ToList();
            int i = 0;
            foreach (Places place in places)
            {
                i++;
                GetProperties("59", "0", "1", place);
                Console.WriteLine(i + ".OK:" + place.Id + "-" + place.Name);
            }
            //Places place = db.Places.Find(1392);
            //GetProperties("59", "0", "1", place);
            Console.WriteLine("Toplam " + totalAddedPlace + " özellik ve kategori eklendi");
            Console.ReadLine();
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
        private static void GetProperties(string cityId, string start, string count, Places place)
        {
            var response = ZomatoInfo.SearchRestaurants(cityId, start, count, place.Name);
            PropertiesDatabaseSave(response, place);
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
                if (!db.Places.Any(x => x.Name.ToLower() == restaurant.Name.ToLower()) && restaurant.Name != null && restaurant.Name != "")
                {
                    if (restaurant.Name.Length > 50)
                        place.Name = restaurant.Name.Substring(0, 50);
                    else
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
        private static void PropertiesDatabaseSave(RestaurantList list, Places place)
        {
            StartAppEntities db = new StartAppEntities();
            if(list==null)
            {
                return;
            }
            var restaurants = list.Restaurants;
            if (restaurants.Length == 0)
            {
                return;
            }
            var restaurant = restaurants[0].Restaurant;
            if (place.Name.ToLower() == restaurant.Name.ToLower() && place.Latitude == double.Parse(restaurant.Location.Latitude) && place.Longitude == double.Parse(restaurant.Location.Longitude))
            {
                List<string> properties = restaurant.Highlights;
                List<string> cuisines = restaurant.Cuisines.Split(',').ToList();

                //categories                
                foreach (var p in properties)
                {
                    Categories cat = new Categories();
                    if (p.ToLower().Contains("luxury dining"))
                    {
                        cat = db.Categories.Find(11);
                    }
                    else if (p.ToLower().Contains("romantic dining"))
                    {
                        cat = db.Categories.Find(8);
                    }
                    else if (p.ToLower().Contains("nightlife"))
                    {
                        cat = db.Categories.Find(12);
                    }
                    else if (p.ToLower().Contains("view"))
                    {
                        cat = db.Categories.Find(21);
                    }
                    else if (p.ToLower().Contains("desserts"))
                    {
                        cat = db.Categories.Find(15);
                    }

                    if (cat.Name != null)
                    {
                        PlaceCategories pc = new PlaceCategories();
                        pc.CategoryId = cat.Id;
                        pc.PlaceId = place.Id;
                        pc.IsActive = true;
                        db.PlaceCategories.Add(pc);
                        totalAddedPlace++;
                    }


                }

                foreach (var c in cuisines)
                {
                    Categories cat = new Categories();
                    if (c.ToLower().Contains("cafe"))
                    {
                        cat = db.Categories.Find(17);
                    }
                    else if (c.ToLower().Contains("fast food"))
                    {
                        cat = db.Categories.Find(19);
                    }
                    if (cat.Name != null)
                    {
                        PlaceCategories pc = new PlaceCategories();
                        pc.CategoryId = cat.Id;
                        pc.PlaceId = place.Id;
                        pc.IsActive = true;
                        db.PlaceCategories.Add(pc);
                        totalAddedPlace++;

                    }
                }

                //properties

                foreach (var p in properties)
                {
                    Properties prop = new Properties();

                    if (p.ToLower().Contains("wifi"))
                    {
                        prop = db.Properties.Find(9);
                    }
                    else if (p.ToLower().Contains("serves alcohol"))
                    {
                        prop = db.Properties.Find(14);
                    }
                    else if (p.ToLower().Contains("breakfast"))
                    {
                        prop = db.Properties.Find(5);
                    }
                    else if (p.ToLower().Contains("valet parking available"))
                    {
                        prop = db.Properties.Find(17);
                    }
                    else if (p.ToLower().Contains("outdoor seating"))
                    {
                        prop = db.Properties.Find(8);
                    }
                    else if (p.ToLower().Contains("bosphorus view"))
                    {
                        prop = db.Properties.Find(23);
                    }
                    else if (p.ToLower().Contains("smoking area"))
                    {
                        prop = db.Properties.Find(11);
                    }
                    else if (p.ToLower().Contains("takeaway available"))
                    {
                        prop = db.Properties.Find(27);
                    }
                    else if (p.ToLower().Contains("desserts and bakes"))
                    {
                        prop = db.Properties.Find(25);
                    }
                    else if (p.ToLower().Contains("rooftop"))
                    {
                        prop = db.Properties.Find(15);
                    }
                    else if (p.ToLower().Contains("pet friendly"))
                    {
                        prop = db.Properties.Find(33);
                    }
                    else if (p.ToLower().Contains("reservation"))
                    {
                        prop = db.Properties.Find(19);
                    }
                    else if (p.ToLower().Contains("live music"))
                    {
                        prop = db.Properties.Find(21);
                    }
                    else if (p.ToLower().Contains("praying room"))
                    {
                        prop = db.Properties.Find(22);
                    }
                    else if (p.ToLower().Contains("live sports screening"))
                    {
                        prop = db.Properties.Find(13);
                    }
                    else if (p.ToLower().Contains("kid friendly"))
                    {
                        prop = db.Properties.Find(31);
                    }
                    else if (p.ToLower().Contains("group meal"))
                    {
                        prop = db.Properties.Find(22);
                    }
                    else if (p.ToLower().Contains("shisha"))
                    {
                        prop = db.Properties.Find(22);
                    }
                    else if (p.ToLower().Contains("locally sourced"))
                    {
                        prop = db.Properties.Find(36);
                    }
                    else if (p.ToLower().Contains("private parking"))
                    {
                        prop = db.Properties.Find(4);
                    }
                    else if (p.ToLower().Contains("board games"))
                    {
                        prop = db.Properties.Find(20);
                    }
                    else if (p.ToLower().Contains("self service"))
                    {
                        prop = db.Properties.Find(20);
                    }

                    if (prop.Name != null)
                    {
                        PlaceProperties pp = new PlaceProperties();
                        pp.PropertyId = prop.Id;
                        pp.PlaceId = place.Id;
                        pp.IsActive = true;
                        db.PlaceProperties.Add(pp);
                        totalAddedPlace++;
                    }
                }

                foreach (var c in cuisines)
                {
                    Properties prop = new Properties();

                    if (c.ToLower().Contains("bar") || c.ToLower().Contains("pub"))
                    {
                        prop = db.Properties.Find(28);
                    }
                    else if (c.ToLower().Contains("korean") || c.ToLower().Contains("chinese") || c.ToLower().Contains("asian") || c.ToLower().Contains("sushi") || c.ToLower().Contains("japanese"))
                    {
                        prop = db.Properties.Find(34);
                    }
                    else if (c.ToLower().Contains("italian"))
                    {
                        prop = db.Properties.Find(35);
                    }
                    else if (c.ToLower().Contains("arabian") || c.ToLower().Contains("labanese"))
                    {
                        prop = db.Properties.Find(37);
                    }
                    else if (c.ToLower().Contains("ottoman"))
                    {
                        prop = db.Properties.Find(38);
                    }
                    else if (c.ToLower().Contains("turkish") || c.ToLower().Contains("kebab") || c.ToLower().Contains("Turkish Pizza"))
                    {
                        prop = db.Properties.Find(36);
                    }

                    if (prop.Name != null)
                    {
                        PlaceProperties pp = new PlaceProperties();
                        pp.PropertyId = prop.Id;
                        pp.PlaceId = place.Id;
                        pp.IsActive = true;
                        db.PlaceProperties.Add(pp);
                        totalAddedPlace++;
                    }
                }
                db.SaveChanges();
            }


            //foreach (var r in restaurants)
            //{
            //    var restaurant = r.Restaurant;
            //    place = new Places();

            //    //if(true)
            //    if (!db.Places.Any(x => x.Name.ToLower() == restaurant.Name.ToLower()) && restaurant.Name != null && restaurant.Name != "")
            //    {
            //        if (restaurant.Name.Length > 50)
            //            place.Name = restaurant.Name.Substring(0, 50);
            //        else
            //            place.Name = restaurant.Name;


            //        var districtList = db.Districts.Where(x => x.IsActive && restaurant.Location.Locality.ToLower().Contains(x.Name.ToLower()));
            //        if (districtList.Count() > 0)
            //        {
            //            place.DistrictId = districtList.FirstOrDefault().Id;
            //        }
            //        else
            //        {
            //            Districts nD = new Districts();
            //            nD.CityId = 1;
            //            nD.Name = restaurant.Location.Locality;
            //            nD.IsActive = true;
            //            db.Districts.Add(nD);
            //            db.SaveChanges();
            //            place.DistrictId = nD.Id;
            //        }

            //        place.AveragePrice = restaurant.AverageCostForTwo;
            //        place.Address = restaurant.Location.Address;
            //        place.PlaceTypeID = 1;
            //        place.Phone = restaurant.Phone.Split(',').First();
            //        place.ShortDescription = "";
            //        place.Description = "";
            //        place.Latitude = double.Parse(restaurant.Location.Latitude);
            //        place.Longitude = double.Parse(restaurant.Location.Longitude);
            //        place.RecordDate = DateTime.Now;
            //        place.IsActive = true;
            //        db.Places.Add(place);
            //        db.SaveChanges();
            //        totalAddedPlace++;
            //    }
            //}
        }
    }
}
