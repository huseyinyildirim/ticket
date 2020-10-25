using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Model;

namespace Data.BLL
{
    public class Base
    {

        public TBL_USER USER
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    var _USER_ID = HttpContext.Current.Session["USER"];
                    if (_USER_ID != null)
                    {
                        TBL_USER a = null;
                        try
                        {
                            a = new BLLUser().GetByID(Convert.ToInt32(_USER_ID));
                        }
                        catch
                        {
                        }
                        return a;
                    }
                }

                return null;

            }
        }

        public PermitClass USER_PERMISSIONS
        {
            get { return new BLLUserPermission().UserPermissions();}
        }

        public bool PERMIT(int point)
        {
            return USER_PERMISSIONS.Check(point);
        }

        public List<TBL_PERMISSION> USER_MENU()
        {
            return USER_PERMISSIONS.Menu;
        }

    }

}
