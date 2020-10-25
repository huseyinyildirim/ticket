using System;
using System.Collections.Generic;
using System.Linq;
using Data.DAL;
using Data.Model;

namespace Data.BLL
{
    public class BLLCity : IDisposable
    {
        private readonly Base<TBL_CITY> _dal = new Base<TBL_CITY>();

        public void Dispose()
        {
            if (_dal != null)
            {
                _dal.Dispose();
            }
        }

        public List<TBL_CITY> Search(TBL_CITY e)
        {
            var q = _dal.Get(k => k.STATUS
            && (e.ID == 0 || k.ID == e.ID)
            && (string.IsNullOrEmpty(e.NAME) || k.NAME.Contains(e.NAME))

            );
            return q;
        }

        public List<TBL_CITY> CityCountyByParentId()
        {
            var q = _dal.Get(k => k.STATUS && k.PARENT_ID != null)
                    .OrderBy(o => o.NAME).Distinct()
                    .ToList();
            return q;
        }

        public List<TBL_CITY> City(int? parentId = null)
        {
            var q = _dal.Get(k => k.STATUS && (parentId == null ? k.PARENT_ID.Equals(null) : k.PARENT_ID == parentId))
                .OrderBy(o => o.NAME)
                .ToList();
            return q;
        }


        public bool GetAny(TBL_CITY e)
        {
            var q = _dal.GetAny(k => k.STATUS
                && (string.IsNullOrEmpty(e.NAME) || k.NAME == e.NAME)
                );
            return q;
        }

        public List<TBL_CITY> GetAll()
        {
            return _dal.Get(k => k.STATUS).OrderBy(o => o.NAME).ToList();
        }

        public TBL_CITY GetByID(int ID)
        {
            return _dal.FirstOrDefault(k => k.ID == ID && k.STATUS);
        }

        public TBL_CITY Add(TBL_CITY entity)
        {
            _dal.Add(entity);
            return entity;
        }

        public void Update(TBL_CITY entity)
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

