using System.Web.Mvc;
using Data.BLL;
using Data.Model;
using Web.Lib;
using Web.Models;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index(string goBack)
        {
            if (USER != null)
            {
                return Redirect(Url.Action("Index", "CP"));
            }

            var model = new LoginModel {GO_BACK = goBack};

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LoginModel form)
        {
            ReturnObject ro;
            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    // Kullanıcı kontrolü
                    var LoginUser = new BLLUser().Control(form.USERNAME, form.PASSWORD.ToMD5());

                    if (LoginUser != null)
                    {
                        // Kullanıcı aktif/pasif kontrolü
                        if (LoginUser.ACTIVE)
                        {
                            // Oturumu aç.
                            Session["USER"] = LoginUser.ID;
                            var returnUrl = form.GO_BACK ?? Url.Action("Index", "CP");
                            ro = new ReturnObject {Code = 0, Url = returnUrl};
                        }
                        else
                        {
                            // Kullanıcı kayıtlı ama sistme girişi engellenmiş.
                            ro = new ReturnObject
                            {
                                Code = 1,
                                Message = "Yönetici tarafından sisteme girişiniz engellenmiştir."
                            };
                        }

                        return Json(new {ro});
                    }

                    ro = new ReturnObject {Code = 1, Message = "Sistemde bu bilgilere ait bir kullanıcı bulunamadı."};
                    return Json(new {ro});
                }

                return null;
            }

            ro = new ReturnObject {Code = 1, Message = "Model valid değil."};
            return Json(new {ro});
        }
    }
}