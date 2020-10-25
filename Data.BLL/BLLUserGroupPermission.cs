using System;
using System.Collections.Generic;
using Data.DAL;
using Data.Model;

namespace Data.BLL
{
    public class BLLUserGroupPermission : IDisposable
    {
        private readonly Base<TBL_USERGROUP_PERMISSION> _dal = new Base<TBL_USERGROUP_PERMISSION>();

        public void Dispose()
        {
            if (_dal != null)
            {
                _dal.Dispose();
            }
        }

        public TBL_USERGROUP_PERMISSION GetByID(int ID)
        {
            return _dal.FirstOrDefault(k => k.ID == ID);
        }

        public List<TBL_USERGROUP_PERMISSION> Search(int? userGroupId = null, int? permissionId = null)
        {
            var q = _dal.Get(k => (userGroupId == null || k.USERGROUP_ID == userGroupId) && (permissionId == null || k.PERMISSION_ID == permissionId));
            return q;
        }

        public TBL_USERGROUP_PERMISSION Add(TBL_USERGROUP_PERMISSION entity)
        {
            _dal.Add(entity);
            return entity;
        }

        public void Update(TBL_USERGROUP_PERMISSION entity)
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
}
