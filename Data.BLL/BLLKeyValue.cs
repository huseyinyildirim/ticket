using System;
using System.Collections.Generic;
using System.Linq;
using Data.DAL;
using Data.Model;

namespace Data.BLL
{
    public class BLLKeyValue : IDisposable
    {
        private readonly Base<REF_KEYVALUE> _dal = new Base<REF_KEYVALUE>();

        public void Dispose()
        {
            if (_dal != null)
            {
                _dal.Dispose();
            }
        }

        public List<REF_KEYVALUE> Search(REF_KEYVALUE e)
        {
            var q = _dal.Get(k => k.STATUS
                && (string.IsNullOrEmpty(e.NAME) || k.NAME.Contains(e.NAME))
                && (k.PARENT_ID.Equals(e.PARENT_ID))
                ).OrderBy(o => o.NAME).ToList();
            return q;
        }
        public List<REF_KEYVALUE> GetKeyvaluesByParentId(int? parentId = null)
        {
            var q = _dal.Get(k => k.STATUS && (parentId == null ? k.PARENT_ID.Equals(null) : k.PARENT_ID == parentId)).OrderBy(o => o.NAME).ToList();
            return q;
        }

        public bool GetAny(REF_KEYVALUE e)
        {
            var q = _dal.GetAny(k => k.STATUS
                && k.NAME == e.NAME
                && k.PARENT_ID == e.PARENT_ID
                );
            return q;
        }

        public List<REF_KEYVALUE> GetAll()
        {
            return _dal.Get(k => k.STATUS).OrderBy(o => o.NAME).ToList();
        }

        public REF_KEYVALUE GetByID(int ID)
        {
            return _dal.FirstOrDefault(k => k.ID == ID && k.STATUS);
        }

        public REF_KEYVALUE Add(REF_KEYVALUE entity)
        {
            _dal.Add(entity);
            return entity;
        }

        public void Update(REF_KEYVALUE entity)
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
