using System;
using System.Collections.Generic;
using Data.DAL;
using Data.Model;

namespace Data.BLL
{
    public class BLLPermission : IDisposable
    {
        private readonly Base<TBL_PERMISSION> _dal = new Base<TBL_PERMISSION>();

        public void Dispose()
        {
            if (_dal != null)
            {
                _dal.Dispose();
            }
        }
        public List<TBL_PERMISSION> GetAll()
        {
            return _dal.Get();
        }

        public TBL_PERMISSION GetByID(int ID)
        {
            return _dal.FirstOrDefault(k => k.ID == ID);
        }

        public List<TBL_PERMISSION> GetPermissionsByParentID(int parentId)
        {
            return _dal.Get(k => k.PARENT_ID == parentId);
        }

        public List<TBL_PERMISSION> GetPermissionsByParentID(int? parentId, bool nullable)
        {
            return _dal.Get(k => k.PARENT_ID.Equals(parentId));
        }

    }
}
