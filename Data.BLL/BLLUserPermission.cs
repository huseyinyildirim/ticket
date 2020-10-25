using System;
using System.Collections.Generic;
using System.Linq;
using Data.DAL;
using Data.Model;

namespace Data.BLL
{
    public class BLLUserPermission : BaseBLL, IDisposable
    {
        private readonly Base<TBL_USER_PERMISSION> _dal = new Base<TBL_USER_PERMISSION>();

        public void Dispose()
        {
            if (_dal != null)
            {
                _dal.Dispose();
            }
        }

        public List<TBL_USER_PERMISSION> GetUserPermissions(int? id = null, int? permissionId = null)
        {
            var userId = id ?? USER.ID;

            var q = _dal.Get(k => k.USER_ID == userId && (permissionId == null || k.PERMISSION_ID == permissionId));
            return q;
        }

        public PermitClass UserPermissions(List<TBL_USER_PERMISSION> userPerms = null)
        {
            var pc = new PermitClass {Permissions = new List<TBL_PERMISSION>() };


            var getUserGroup = userPerms ?? GetUserPermissions();
            var groups = getUserGroup.Where(w => !w.USERGROUP_ID.Equals(null)).ToList();
            var excludePerms = getUserGroup.Where(w => !w.PERMISSION_ID.Equals(null)).ToList();

            // kullanıcının ait olduğu grupları listeleyip yetkilerini topluyoruz. Örn: Administrator.
            foreach (var item in groups)
            {
                var usergroupPermissions = item.TBL_USERGROUP.TBL_USERGROUP_PERMISSION;

                // grubun yetkileri. Örn: Administrator => Yönetim
                foreach (var item2 in usergroupPermissions)
                {
                    pc.Permissions.Add(item2.TBL_PERMISSION);

                    // grubun yetkilerinin alt yetkileri. Örn: Administrator => Yönetim =>  Sabit tanımlar
                    using (var bllPermission = new BLLPermission())
                    {
                        var subPermissions = bllPermission.GetPermissionsByParentID(item2.TBL_PERMISSION.ID);
                        foreach (var item3 in subPermissions)
                        {
                            pc.Permissions.Add(item3);

                            // grubun yetkilerinin alt yetkilerinin alt yetkileri. Örn: Administrator => Yönetim =>  Sabit tanımlar => Sabit tanım ekleme
                            var subsubPermissions = bllPermission.GetPermissionsByParentID(item3.ID);
                            foreach (var item4 in subsubPermissions)
                            {
                                pc.Permissions.Add(item4);
                            }

                        }                    
                    }
                    
                }

            }


            // +++++ oluşan listeyi gruplayalım ki kayıtlar çoklamasın

            var grouppedPerms = pc.Permissions.GroupBy(g => g.ID).ToList();

            pc.Permissions.Clear();
            foreach (var item in grouppedPerms)
            {
                pc.Permissions.Add(item.First());
            }

            // ----- oluşan listeyi gruplayalım ki kayıtlar çoklamasın


            // +++++ eksiltilen yetkileri listeden siliyoruz

            foreach (var item in excludePerms)
            {

                // Silmeden önce bakalım alt yetkileri de varsa onları silelim.
                foreach (var subItem in item.TBL_PERMISSION.TBL_PERMISSION2)
                {
                    var found1 = pc.Permissions.FirstOrDefault(f => f.ID == subItem.ID);
                    if (found1 != null)
                    {
                        pc.Permissions.Remove(found1);                    
                    } 
                }

                var found2 = pc.Permissions.FirstOrDefault(f => f.ID == item.TBL_PERMISSION.ID);
                if (found2 != null)
                {
                    pc.Permissions.Remove(found2);                   
                }
            }

            // ----- eksiltilen yetkileri listeden siliyoruz

            return pc;
        } 

        public TBL_USER_PERMISSION GetByID(int ID)
        {
            return _dal.FirstOrDefault(k => k.ID == ID);
        }

        public TBL_USER_PERMISSION Add(TBL_USER_PERMISSION entity)
        {
            _dal.Add(entity);
            return entity;
        }

        public void Update(TBL_USER_PERMISSION entity)
        {
            _dal.Update(entity);
        }

        public void Delete(int ID)
        {
            _dal.Delete(ID);
        }
        public void Delete(int ID, bool realDelete)
        {
            _dal.Delete(ID, true);
        }
    }

    public class PermitClass
    {
        public int[] Points { get { return Permissions.Select(s => s.ID).ToArray(); } }
        public bool Check (int point) {  return Points.Any(a => a.Equals(point));}
        public List<TBL_PERMISSION> Permissions { get; set; }
        public List<TBL_PERMISSION> Menu { get { return Permissions.Where(w => w.ISMENU == true).ToList(); } }
    }
}
