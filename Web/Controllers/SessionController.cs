using System.Web.Mvc;

namespace Web.Controllers
{
    public class SessionController : Controller
    {
        public ActionResult Index()
        {
            return Content("OK");
        }
	}
}