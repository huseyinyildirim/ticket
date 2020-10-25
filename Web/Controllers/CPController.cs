using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Data.BLL;
using Data.Model;
using Web.Lib;

namespace Web.Controllers
{
    public class CPController : CPBaseController
    {

        public ActionResult Index()
        {

            if (PERMIT(24))
            {
                var DD = "DFGDFG";
                
            }


            if (PERMIT_GROUP(Enum_UserGroups.Administrator))
            {
                
            }

            return View();
        }

        public ActionResult Exit()
        {
            Session.Abandon();

            return Redirect(Url.Action("Index", "Login"));
        }

        public ActionResult GetCountys(int id)
        {
            var body = "";
            var q = new BLLCity().City(id);
            if (q.Any())
            {
                foreach (var item in q)
                {
                    body += "<option value=\"" + item.ID + "\" data-lat=\"" + item.LAT + "\" data-long=\"" + item.LONG +
                            "\">" + item.NAME + "</option>";
                }
            }
            else
            {
                body = "<option>İlçe yok.</option>";
            }

            return Content(body);

        }

    }

}