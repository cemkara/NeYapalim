using StartAppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //--mekanlara kategori eklenmesi
            //--cafelere çay,kahve,meşrubat ekle
            //--alkol olan ve pub bar olan mekanlara bira,viski ekle
            //--meyhanelere rakı,bira ekle
            StartAppEntities db = new StartAppEntities();

            //eklendi: cafelere çay kahve meşrubat
            //IQueryable<Places> cafePlaces = db.PlaceCategories.Where(x => x.CategoryId == 17).Select(x => x.Places);
            //foreach (Places place in cafePlaces)
            //{
            //    PlaceCategories pCat = new PlaceCategories();
            //    pCat.PlaceId = place.Id;
            //    pCat.CategoryId = 23; 
            //    pCat.Note = "";
            //    pCat.IsActive = true;
            //    db.PlaceCategories.Add(pCat);

            //    PlaceCategories pCat2 = new PlaceCategories();
            //    pCat2.PlaceId = place.Id;
            //    pCat2.CategoryId = 24; 
            //    pCat2.Note = "";
            //    pCat2.IsActive = true;
            //    db.PlaceCategories.Add(pCat2);

            //    PlaceCategories pCat3 = new PlaceCategories();
            //    pCat3.PlaceId = place.Id;
            //    pCat3.CategoryId = 29; 
            //    pCat3.Note = "";
            //    pCat3.IsActive = true;
            //    db.PlaceCategories.Add(pCat3);
            //    Console.WriteLine("ok");
            //}

            //eklendi:meyhaneler: rakı,bira
            //IQueryable<Places> meyhanePlaces = db.Places.Where(x => x.Name.Contains("meyhane"));
            //foreach (Places place in meyhanePlaces)
            //{
            //    PlaceCategories pCat = new PlaceCategories();
            //    pCat.PlaceId = place.Id;
            //    pCat.CategoryId = 25; 
            //    pCat.Note = "";
            //    pCat.IsActive = true;
            //    db.PlaceCategories.Add(pCat);

            //    PlaceCategories pCat2 = new PlaceCategories();
            //    pCat2.PlaceId = place.Id;
            //    pCat2.CategoryId = 26; 
            //    pCat2.Note = "";
            //    pCat2.IsActive = true;
            //    db.PlaceCategories.Add(pCat2);
            //}

            //eklendi: alkollü mekan & pubbar:bira viski
            //IQueryable<Places> alcholPlaces = db.PlaceProperties.Where(x => x.PropertyId == 14 || x.PropertyId == 28).Select(x => x.Places);
            //foreach (Places place in alcholPlaces)
            //{
            //    PlaceCategories pCat = new PlaceCategories();
            //    pCat.PlaceId = place.Id;
            //    pCat.CategoryId = 26;
            //    pCat.Note = "";
            //    pCat.IsActive = true;
            //    db.PlaceCategories.Add(pCat);

            //    PlaceCategories pCat2 = new PlaceCategories();
            //    pCat2.PlaceId = place.Id;
            //    pCat2.CategoryId = 32;
            //    pCat2.Note = "";
            //    pCat2.IsActive = true;
            //    db.PlaceCategories.Add(pCat2);
            //}

            db.SaveChanges();
            Console.WriteLine("complated");
            Console.Read();
        }
    }
}
