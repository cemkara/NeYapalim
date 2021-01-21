using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourcePlaces
{
    public static class ZomatoInfo
    {
        const string url = "https://developers.zomato.com/api/v2.1/";
        const string apiKey = "0d8572d04e3df8c1fa6f1ac1da796ef3";

        public static RestaurantList GetRestaurants(string cityId, string start, string count)
        {
            string urlParameters = $"search?entity_id={cityId}&entity_type=city&start={start}&count={count}&apikey={apiKey}";
            var response = APICall.RunAsync<RestaurantList>(url, urlParameters).GetAwaiter().GetResult();
            return response;
        }

        public static RestaurantList SearchRestaurants(string cityId, string start, string count, string name)
        {
            string urlParameters = $"search?entity_id={cityId}&entity_type=city&q={name}&start={start}&count={count}&apikey={apiKey}";
            var response = APICall.RunAsync<RestaurantList>(url, urlParameters).GetAwaiter().GetResult();
            return response;
        }
    }
}
