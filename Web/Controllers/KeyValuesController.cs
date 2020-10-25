using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.BLL;
using Data.Model;
using Web.Lib;

namespace Web.Controllers
{
    public class KeyValuesController : CPBaseController
    {
        public ActionResult Index(int? id, string mode)
        {

            var model = new MODEL_KEYVALUE_Index {REF_KEYVALUE = new List<REF_KEYVALUE>(), SEARCH_FORM = new REF_KEYVALUE(), ADD_FORM = new ADD_FORM{FORM = new REF_KEYVALUE()}, EDIT_FORM = new EDIT_FORM{ FORM = new REF_KEYVALUE()}};

            var bllKeyvalue = new BLLKeyValue();

            model.REF_KEYVALUE.AddRange(bllKeyvalue.GetKeyvaluesByParentId());
            if (mode == "edit" && id != null && id > 0)
            {
                ViewBag.IsEdit = true;
                model.EDIT_FORM.FORM = bllKeyvalue.GetByID((int) id);
            }
            else if (string.IsNullOrEmpty(mode) && id > 0)
            {
                ViewBag.IsAdd = true;
                model.ADD_FORM.FORM.PARENT_ID = id;
            }
            
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MODEL_KEYVALUE_Index model)
        {
            model.EDIT_FORM = new EDIT_FORM{FORM = new REF_KEYVALUE()};
            model.ADD_FORM = new ADD_FORM{FORM = new REF_KEYVALUE()};
            model.REF_KEYVALUE = new BLLKeyValue().Search(model.SEARCH_FORM);

            return View(model);
        }

        public ActionResult Add(MODEL_KEYVALUE_Index model)
        {
            ReturnObject ro;

            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {

                    using (var bllKeyvalue = new BLLKeyValue())
                    {
                        var usr = bllKeyvalue.GetAny(model.ADD_FORM.FORM);
                        if (usr)
                        {
                            ro = new ReturnObject { Code = 1, Message = "Belirtilen Tanım bu ana başlık altında zaten kayıtlı." };
                            return Json(new { ro });
                        }

                        bllKeyvalue.Add(model.ADD_FORM.FORM);
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
        public ActionResult Edit(MODEL_KEYVALUE_Index model, int id)
        {
            ReturnObject ro;


            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    using (var bllKeyvalue = new BLLKeyValue())
                    {
                        var usr = bllKeyvalue.GetAny(model.EDIT_FORM.FORM);
                        if (usr)
                        {
                            ro = new ReturnObject { Code = 1, Message = "Belirtilen Tanım bu ana başlık altında zaten kayıtlı." };
                            return Json(new { ro });
                        }

                        var q = bllKeyvalue.GetByID(id);
                        q.NAME = model.EDIT_FORM.FORM.NAME;
                        bllKeyvalue.Update(q);

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
            using (var bllKeyvalue = new BLLKeyValue())
            {
                var q = bllKeyvalue.GetByID(id);
                if (q.PARENT_ID != null)
                {
                    bllKeyvalue.Delete(id);
                }
            }

            return Redirect(Url.Action("Index"));
        }

        public class MODEL_KEYVALUE_Index
        {
            public REF_KEYVALUE SEARCH_FORM { get; set; }
            public ADD_FORM ADD_FORM { get; set; }
            public EDIT_FORM EDIT_FORM { get; set; }
            public List<REF_KEYVALUE> REF_KEYVALUE { get; set; }
        }

        public class ADD_FORM
        {
            public REF_KEYVALUE FORM { get; set; }
        }
        public class EDIT_FORM
        {
            public REF_KEYVALUE FORM { get; set; }
        }
	}
}