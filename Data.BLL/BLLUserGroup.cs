using System;
using System.Collections.Generic;
using System.Linq;
using Data.DAL;
using Data.Model;

namespace Data.BLL
{
    public class BLLUserGroup : IDisposable
    {
        private readonly Base<TBL_USERGROUP> _dal = new Base<TBL_USERGROUP>();

        public void Dispose()
        {
            if (_dal != null)
            {
                _dal.Dispose();
            }
        }


        public bool GetAny(TBL_USERGROUP e)
        {
            return _dal.GetAny(k => k.NAME == e.NAME);
        }

        public TBL_USERGROUP GetByID(int ID)
        {
            return _dal.FirstOrDefault(k => k.ID == ID);
        }

        public List<TBL_USERGROUP> GetAll()
        {
            return _dal.Get().OrderBy(o => o.NAME).ToList();
        }

        public TBL_USERGROUP Add(TBL_USERGROUP entity)
        {
            _dal.Add(entity);
            return entity;
        }

        public void Update(TBL_USERGROUP entity)
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
