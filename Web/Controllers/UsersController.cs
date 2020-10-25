using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Data.BLL;
using Data.Model;
using Web.Lib;

namespace Web.Controllers
{
    public class UsersController : CPBaseController
    {
        public ActionResult Index(int? userId)
        {
            var model = new MODEL_USERS_Index
            {
                TBL_USER = new List<TBL_USER>(),
                SEARCH_FORM = new TBL_USER(),
                ADD_FORM = new ADD_FORM {FORM = new TBL_USER()},
                EDIT_FORM = new EDIT_FORM {FORM = new TBL_USER()}
            };

            if (userId != null && userId > 0)
            {
                ViewBag.IsEdit = true;
                model.EDIT_FORM.FORM = new BLLUser().GetByID((int) userId);
                model.EDIT_FORM.FORM.PASSWORD = "";
            }

            model.TBL_USER = new BLLUser().GetAll();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MODEL_USERS_Index model)
        {
            model.EDIT_FORM = new EDIT_FORM {FORM = new TBL_USER()};
            model.TBL_USER = new BLLUser().Search(model.SEARCH_FORM, userGroupId: model.USERGROUP_ID);
            return View(model);
        }

        public ActionResult ActiveChange(int id)
        {
            using (var bllUser = new BLLUser())
            {
                var q = bllUser.GetByID(id);
                q.ACTIVE = (!q.ACTIVE);
                bllUser.Update(q);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(MODEL_USERS_Index model)
        {
            ReturnObject ro;

            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {

                    if (model.ADD_FORM.USERGROUP_ID == 0)
                    {
                        ro = new ReturnObject
                        {
                            Code = 1,
                            Message =
                                "Kullanıcı için bir yetki grubu seçmelisiniz. Yetkilerini arttırmak/azaltmak için kullanıcıyı ekledikten sonra Yetkileri bölümünden gerekli işlemleri yapabilirsiniz."
                        };
                        return Json(new {ro});
                    }

                    using (var bllUser = new BLLUser())
                    {

                        var usr = bllUser.UserAddEditAnyControl(model.EDIT_FORM.FORM.USERNAME,
                            model.EDIT_FORM.FORM.EMAIL);
                        if (usr)
                        {
                            ro = new ReturnObject
                            {
                                Code = 1,
                                Message = "Belirtilen Kullanıcı Adı / E-Posta adresi sistemte zaten kayıtlı."
                            };
                            return Json(new {ro});
                        }

                        model.ADD_FORM.FORM.ADDED_DATETIME = DateTime.Now;
                        model.ADD_FORM.FORM.ACTIVE = true;
                        model.ADD_FORM.FORM.PASSWORD = model.ADD_FORM.FORM.PASSWORD.ToMD5();
                        var q = bllUser.Add(model.ADD_FORM.FORM);

                        using (var bllUserPermission = new BLLUserPermission())
                        {
                            bllUserPermission.Add(new TBL_USER_PERMISSION
                            {
                                USER_ID = q.ID,
                                USERGROUP_ID = model.ADD_FORM.USERGROUP_ID
                            });
                        }

                    }

                    ro = new ReturnObject {Code = 0, Url = Url.Action("Index")};
                    return Json(new {ro});
                }

                return null;
            }

            ro = new ReturnObject {Code = 1, Message = "Model valid değil."};
            return Json(new {ro});
        }

        [HttpPost]
        public ActionResult Edit(MODEL_USERS_Index model, int id)
        {
            ReturnObject ro;

            // Zorunlu alanları devredışı bırakmak için
            ModelState.Remove("EDIT_FORM.FORM.PASSWORD");

            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    using (var bllUser = new BLLUser())
                    {
                        var q = bllUser.GetByID(id);

                        q.NAME = model.EDIT_FORM.FORM.NAME;
                        q.SURNAME = model.EDIT_FORM.FORM.SURNAME;
                        q.EMAIL = model.EDIT_FORM.FORM.EMAIL;
                        q.USERNAME = model.EDIT_FORM.FORM.USERNAME;

                        if (!string.IsNullOrEmpty(model.EDIT_FORM.FORM.PASSWORD))
                        {
                            q.PASSWORD = model.EDIT_FORM.FORM.PASSWORD.ToMD5();
                        }

                        bllUser.Update(q);

                    }

                    ro = new ReturnObject {Code = 0, Url = Url.Action("Index")};
                    return Json(new {ro});
                }

                return null;
            }

            ro = new ReturnObject {Code = 1, Message = "Model valid değil."};
            return Json(new {ro});
        }

        public ActionResult Permissions(int id)
        {
            var model = new MODEL_USERS_PERMISSIONS_Index
            {
                TBL_USER = new TBL_USER(),
                TBL_USER_PERMISSION = new List<TBL_USER_PERMISSION>()
            };
            model.TBL_USER_PERMISSION.AddRange(new BLLUserPermission().GetUserPermissions(id));
            model.TBL_USER = new BLLUser().GetByID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult PermissionChange(int userId, int permissionId)
        {
            var bllUserPermission = new BLLUserPermission();
            var bllPermission = new BLLPermission();
            var returnPerms = new List<int>();
            var userPermList = bllUserPermission.GetUserPermissions(userId);
            var thisPermission = bllPermission.GetByID(permissionId);

            // Seçilen ID'yi ekle
            returnPerms.Add(permissionId);

            // Seçilen ID'nin sub'ları varsa onları da ekle
            var subPerms = bllPermission.GetPermissionsByParentID(permissionId);
            foreach (var subPerm in subPerms)
            {
                if (userPermList.All(a => a.PERMISSION_ID != subPerm.ID))
                {
                    returnPerms.Add(subPerm.ID);
                }

            }

            if (thisPermission.PARENT_ID != null)
            {
                if (userPermList.Any(a => a.PERMISSION_ID == thisPermission.PARENT_ID))
                {
                    returnPerms.Clear();
                }
            }

            if (returnPerms.Any())
            {

                // bu kullanıcıda bu yetki var mı ? varsa sil, yoksa ekle.
                var permList = bllUserPermission.GetUserPermissions(userId, permissionId);

                if (permList.Any())
                {
                    foreach (var item in permList)
                    {
                        bllUserPermission.Delete(item.ID, true);
                    }
                }
                else
                {
                    bllUserPermission.Add(new TBL_USER_PERMISSION {USER_ID = userId, PERMISSION_ID = permissionId});
                }

            }

            return Json(new {returnPerms});

        }

        public ActionResult PermissionUsergroupDelete(int id, int USERID)
        {
            using (var bllUserPermission = new BLLUserPermission())
            {
                bllUserPermission.Delete(id, true);
            }
            return Redirect(Url.Action("Permissions", new {id = USERID}));
        }

        public ActionResult PermissionsAllDelete(int USERID)
        {

            using (var bllUserPermission = new BLLUserPermission())
            {
                var perms = bllUserPermission.GetUserPermissions(USERID);
                foreach (var perm in perms)
                {
                    bllUserPermission.Delete(perm.ID, true);
                }
            }

            return Redirect(Url.Action("Permissions", new {id = USERID}));

        }

        public ActionResult UserGroupAdd(int USERID, int permissionId)
        {
            using (var bllUserPermission = new BLLUserPermission())
            {
                bllUserPermission.Add(new TBL_USER_PERMISSION {USERGROUP_ID = permissionId, USER_ID = USERID});
            }

            return Redirect(Url.Action("Permissions", new {id = USERID}));
        }

        public ActionResult Delete(int id)
        {
            using (var bllUser = new BLLUser())
            {
                var q = bllUser.GetByID(id);
                if (q.TBL_USER_PERMISSION.All(a => a.USERGROUP_ID != (int) Enum_UserGroups.Administrator))
                {
                    bllUser.Delete(id);
                }
            }

            return RedirectToAction("Index");

        }

        public class MODEL_USERS_Index
        {
            public TBL_USER SEARCH_FORM { get; set; }
            public ADD_FORM ADD_FORM { get; set; }
            public EDIT_FORM EDIT_FORM { get; set; }
            public int? USERGROUP_ID { get; set; }
            public List<TBL_USER> TBL_USER { get; set; }
        }

        public class ADD_FORM
        {
            public TBL_USER FORM { get; set; }

            [Display(Name = "Yetki Grubu")]
            [Required(ErrorMessage = "*")]
            public int USERGROUP_ID { get; set; }
        }

        public class EDIT_FORM
        {
            public TBL_USER FORM { get; set; }
        }

        public class MODEL_USERS_PERMISSIONS_Index
        {
            public List<TBL_USER_PERMISSION> TBL_USER_PERMISSION { get; set; }
            public TBL_USER TBL_USER { get; set; }
        }
    }
}