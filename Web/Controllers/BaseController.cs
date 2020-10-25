using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Data.BLL;
using Data.Model;
using ImageResizer;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public TBL_USER USER
        {
            get
            {
                return new Base().USER;
            }
        }

        public ActionResult ImageResize(string url, int? w, int? h)
        {
            url = Server.MapPath(url);
            var fi = new FileInfo(url);
            if (url == "" || !fi.Exists) url = Server.MapPath("/Content/Images/noImage.png");

            var newWidth = w != null ? (int)w : 0;
            var newHeight = h != null ? (int)h : 0;

            var imgs = Image.FromFile(url);

            if (w == null && h != null)
            {
                newHeight = (int)h;
                newWidth = imgs.Width * newHeight / imgs.Height;
            }
            else if (w != null && h == null)
            {
                newWidth = (int)w;
                newHeight = imgs.Height * newWidth / imgs.Width;
            }

            var img = ImageBuilder.Current.Build(url, new ResizeSettings(newWidth, newHeight, FitMode.Crop, "png"));
            FileContentResult result;

            using (var memStream = new MemoryStream())
            {
                img.Save(memStream, ImageFormat.Png);
                result = File(memStream.GetBuffer(), "image/png");
            }

            return result;
        }

        public Image Base64ToImage(string base64String)
        {
            var imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(ms, true);
            return image;
        }

        public string ImageToBase64(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                var imageBytes = ms.ToArray();
                var base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public string UPLOAD_FILES_PROFILE_IMAGE = @"/Upload/Profile/";

    }
}