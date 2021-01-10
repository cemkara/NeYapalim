using StartAppModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI.WebControls;

namespace StartApp.Cms.App_Start
{
    public static class UploadImage
    {
        private static StartAppEntities db = new StartAppEntities();

        public static bool UploadAndSave(HttpPostedFileBase file, UploadImageType type, int itemId, int width, int height)
        {
            WebImage img = new WebImage(file.InputStream);
            if (img.Width > width)
                img.Resize(width, height);
            try
            {
                img.Save(UploadImage.GetUploadUrl(type, itemId, file.FileName));
                ImageSave(type, itemId, file.FileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string GetUploadUrl(UploadImageType type, int itemId, string filename)
        {
            string retVal = @"C:\Inetpub\vhosts\neyapalim.online\httpdocs\assets\img\";
            switch (type)
            {
                case UploadImageType.Place:
                    retVal += @"img-place\" + itemId + @"\";
                    if (!File.Exists(retVal))
                        Directory.CreateDirectory(retVal);
                    break;
                case UploadImageType.Content:
                    retVal += @"img-content\" + itemId + @"\";
                    if (!File.Exists(retVal))
                        Directory.CreateDirectory(retVal);
                    break;
                case UploadImageType.Blog:
                    retVal += @"img-blog\" + itemId + @"\";
                    if (!File.Exists(retVal))
                        Directory.CreateDirectory(retVal);
                    break;
                default:
                    break;
            }
            return retVal + filename;
        }

        private static void ImageSave(UploadImageType type, int itemId, string filename)
        {
            switch (type)
            {
                case UploadImageType.Place:
                    PlaceImages img = new PlaceImages();
                    img.ImageUrl = @"assets\img\img-place\" + itemId + @"\" + filename;
                    img.MainImage = false;
                    img.PlaceId = itemId;
                    img.RecordDate = DateTime.Now;
                    db.PlaceImages.Add(img);
                    break;
                case UploadImageType.Content:
                    Contents contents = db.Contents.Find(itemId);
                    contents.UpdatedDate = DateTime.Now;
                    contents.ImageUrl = @"assets\img\img-content\" + itemId + @"\" + filename;
                    break;
                case UploadImageType.Blog:
                    Blogs blog = db.Blogs.Find(itemId);
                    blog.ImageUrl = @"assets\img\img-blog\" + itemId + @"\" + filename;
                    break;
                default:
                    break;
            }

            db.SaveChanges();
        }
    }

    public enum UploadImageType
    {
        Place = 1,
        Content = 2,
        Blog = 3
    }
}