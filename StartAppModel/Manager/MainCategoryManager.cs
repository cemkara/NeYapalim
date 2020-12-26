using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartAppModel
{
    public class MainCategoryManager
    {
        public StartAppEntities db = new StartAppEntities();

        /// <summary>
        /// Lists categories on the home page
        /// </summary>
        /// <returns></returns>
        public List<MainCategories> GetMainCategories()
        {
            return db.MainCategories.Where(x=>x.IsActive).ToList();
        }

        /// <summary>
        /// Listing the subcategories of the selected main category
        /// </summary>
        /// <returns></returns>
        public List<Categories> GetCategories(int mainCategoryId)
        {
            return db.Categories.Where(x => x.MainCategoryId == mainCategoryId && x.IsActive).ToList();
        }
    }
}
