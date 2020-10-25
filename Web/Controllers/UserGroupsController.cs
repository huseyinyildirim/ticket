using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Data.BLL;
using Data.Model;
using Web.Lib;

namespace Web.Controllers
{
    public class UserGroupsController : CPBaseController
    {
        public ActionResult Index(int? id)
        {
            var model = new MODEL_USERGROUPS_Index
            {
                TBL_USERGROUP = new BLLUserGroup().GetAll(),
                ADD_FORM = new ADD_FORM() { FORM = new TBL_USERGROUP() },
                EDIT_FORM = new EDIT_FORM() { FORM = new TBL_USERGROUP() }
            };

            if (id != null && id > 0)
            {
                ViewBag.IsEdit = true;
                model.EDIT_FORM.FORM = new BLLUserGroup().GetByID((int)id);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(MODEL_USERGROUPS_Index model)
        {
            ReturnObject ro;

            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {

                    using (var bllUsergroup = new BLLUserGroup())
                    {
                        var usr = bllUsergroup.GetAny(model.ADD_FORM.FORM);
                        if (usr)
                        {
                            ro = new ReturnObject { Code = 1, Message = "Belirtilen Kullanıcı Grubu adı sistemte zaten kayıtlı." };
                            return Json(new { ro });
                        }
                    }


                    using (var bllUsergroup = new BLLUserGroup())
                    {
                        bllUsergroup.Add(model.ADD_FORM.FORM);
                    }

                    ro = new ReturnObject { Code = 0, Url = Url.Action("Index") };
                    return Json(new { ro });
                }

                return null;
            }

            ro = new ReturnObject { Code = 1, Message = "Model valid değil." };
            return Json(new { ro });
        }

        [HttpPost]
        public ActionResult Edit(MODEL_USERGROUPS_Index model, int id)
        {
            ReturnObject ro;


            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    using (var bllUsergroup = new BLLUserGroup())
                    {
                        var usr = bllUsergroup.GetAny(model.EDIT_FORM.FORM);
                        if (usr)
                        {
                            ro = new ReturnObject { Code = 1, Message = "Belirtilen Kullanıcı Grubu adı sistemte zaten kayıtlı." };
                            return Json(new { ro });
                        }
                    }

                    using (var bllUsergroup = new BLLUserGroup())
                    {
                        var q = bllUsergroup.GetByID(id);
                        q.NAME = model.EDIT_FORM.FORM.NAME;
                        q.CODE = model.EDIT_FORM.FORM.CODE;
                        bllUsergroup.Update(q);
                    }

                    ro = new ReturnObject { Code = 0, Url = Url.Action("Index") };
                    return Json(new { ro });
                }

                return null;
            }

            ro = new ReturnObject { Code = 1, Message = "Model valid değil." };
            return Json(new { ro });
        }

        public ActionResult Delete(int id)
        {
            using (var bllUsergroup = new BLLUserGroup())
            {
                bllUsergroup.Delete(id, true);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Permissions(int id)
        {
            var model = new MODEL_USERGROUP_PERMISSIONS_Index {TBL_USERGROUP = new TBL_USERGROUP(), TBL_USERGROUP_PERMISSIONS = new List<TBL_USERGROUP_PERMISSION>(), TBL_PERMISSION_POOL = new List<TBL_PERMISSION>() };

            // Grubun bilgilerini alalım
            var userGroup = new BLLUserGroup().GetByID(id);

            // Kullanılabilir ana yetkileri çekiyoruz
            var usergroupPermissionPool = new BLLPermission().GetPermissionsByParentID(null, true).ToList();

            // Grubun şu an ki yetkilerini çekiyoruz
            var usergroupPermissions = new BLLUserGroupPermission().Search(id).ToList();


            // Kullanılabilir yetkiler arasından grubunkileri çıkartıyoruz
            foreach (var ugPerm in usergroupPermissions)
            {
                var found = usergroupPermissionPool.FirstOrDefault(w => w.ID == ugPerm.PERMISSION_ID);
                if (found != null)
                {
                    usergroupPermissionPool.Remove(found);
                } 
            }

            model.TBL_USERGROUP = userGroup;
            model.TBL_PERMISSION_POOL.AddRange(usergroupPermissionPool);
            model.TBL_USERGROUP_PERMISSIONS.AddRange(usergroupPermissions);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Permissions(int id, string[] permissionId)
        {

            foreach (var p in permissionId)
            {
                using (var bllUsergroupPermission = new BLLUserGroupPermission())
                {
                    bllUsergroupPermission.Add(new TBL_USERGROUP_PERMISSION{PERMISSION_ID = Converting.GetInt32(p), USERGROUP_ID = id});
                }
            }

            return Redirect(Url.Action("Permissions", new { id }));
        }

        public ActionResult UsergroupPermissionDelete(int id, int USERGROUP_ID)
        {
            using (var bllUsergroupPermission = new BLLUserGroupPermission())
            {
                bllUsergroupPermission.Delete(id, true);
            }

            return Redirect(Url.Action("Permissions", new { id = USERGROUP_ID }));
        }

        public class MODEL_USERGROUPS_Index
        {
            public ADD_FORM ADD_FORM { get; set; }
            public EDIT_FORM EDIT_FORM { get; set; }
            public List<TBL_USERGROUP> TBL_USERGROUP { get; set; }
        }

        public class ADD_FORM
        {
            public TBL_USERGROUP FORM { get; set; }
        }

        public class EDIT_FORM
        {
            public TBL_USERGROUP FORM { get; set; }
        }

        public class MODEL_USERGROUP_PERMISSIONS_Index
        {
            public TBL_USERGROUP TBL_USERGROUP { get; set; }
            public List<TBL_PERMISSION> TBL_PERMISSION_POOL { get; set; }
            public List<TBL_USERGROUP_PERMISSION> TBL_USERGROUP_PERMISSIONS { get; set; }
        }

	}
}