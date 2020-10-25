using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Data.BLL;
using Data.Model;
using Web.Lib;

namespace Web.Controllers
{
    [LoginControl]
    [HttpHeader("Access-Control-Allow-Origin", "*")]
    public class CPBaseController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {

            if (USER != null)
            {
                ViewBag.USER = USER;
                ViewBag.USER_MENU = USER_MENU();
            }

            base.Initialize(requestContext);

        }

        public bool PERMIT(int point)
        {
            return new Base().PERMIT(point);
        }

        public bool PERMIT_GROUP(Enum_UserGroups group)
        {
            return USER.TBL_USER_PERMISSION.Any(w => w.USERGROUP_ID == (int) group);
        }
        public List<TBL_PERMISSION> USER_MENU()
        {
            return new Base().USER_MENU();
        }

        public string Upload(HttpPostedFileBase uploadfile, string uploadDirectory)
        {
            var result = "";

            if (uploadfile != null && uploadfile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(uploadfile.FileName).ToFileFormatString();

                var path = Path.Combine(Server.MapPath(uploadDirectory), fileName);
                uploadfile.SaveAs(path);

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".txt", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pps", ".ppsx", ".zip", ".rar", ".pdf", ".7z" };
                var extension = Path.GetExtension(uploadfile.FileName.ToLower());
                if (!allowedExtensions.Contains(extension))
                {
                    result = "Bu dosya formatına izin verilmemektedir.";
                }
                else
                {
                    result = uploadDirectory + fileName;
                }
            } 

            return result;
        }

    }
}