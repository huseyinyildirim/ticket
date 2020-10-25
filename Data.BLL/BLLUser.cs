using System;
using System.Collections.Generic;
using System.Linq;
using Data.DAL;
using Data.Model;

namespace Data.BLL
{
    public class BLLUser : IDisposable
    {
        private readonly Base<TBL_USER> _dal = new Base<TBL_USER>();

        public void Dispose()
        {
            if (_dal != null)
            {
                _dal.Dispose();
            }
        }

        public List<TBL_USER> Search(TBL_USER e, DateTime? startDatetime = null, DateTime? finishDatetime = null, int? userGroupId = 0)
        {
            userGroupId = userGroupId ?? 0;
            finishDatetime = finishDatetime == null ? (DateTime?)null : finishDatetime.Value.AddHours(23).AddMinutes(59).AddSeconds(59);

            var q = _dal.Get(k => k.STATUS
                && (string.IsNullOrEmpty(e.NAME) || (k.NAME == null || k.NAME.ToUpper().Contains(e.NAME.ToUpper())))
                && (string.IsNullOrEmpty(e.SURNAME) || (k.SURNAME == null || k.SURNAME.ToUpper().Contains(e.SURNAME.ToUpper())))
                && (string.IsNullOrEmpty(e.USERNAME) || (k.USERNAME == null || k.USERNAME.ToUpper().Contains(e.USERNAME.ToUpper())))
                && (string.IsNullOrEmpty(e.EMAIL) || (k.EMAIL == null || k.EMAIL.ToUpper().Contains(e.EMAIL.ToUpper())))
                && (userGroupId == 0 || k.TBL_USER_PERMISSION.Any(a => a.USERGROUP_ID == userGroupId))
                ).OrderBy(o => o.NAME).ThenBy(t => t.SURNAME).ToList();
            return q;
        }

        public bool UserAddEditAnyControl(string userName, string eMail)
        {
            var q = _dal.Get(k => String.Equals(k.USERNAME, userName, StringComparison.CurrentCultureIgnoreCase) || String.Equals(k.EMAIL, eMail, StringComparison.CurrentCultureIgnoreCase)).Any();
            return q;
        }

        public List<TBL_USER> GetAll()
        {
            return _dal.Get(k => k.STATUS).OrderBy(o => o.NAME).ToList();
        }

        public TBL_USER GetByID(int ID)
        {
            return _dal.FirstOrDefault(k => k.ID == ID && k.STATUS);
        }

        public TBL_USER Control(string userName, string password)
        {
            return _dal.Get(k => (k.USERNAME == userName || k.EMAIL == userName) && k.PASSWORD == password).FirstOrDefault();
        }

        public TBL_USER Add(TBL_USER entity)
        {
            _dal.Add(entity);
            return entity;
        }

        public void Update(TBL_USER entity)
        {
            _dal.Update(entity);
        }

        public void Delete(int ID)
        {
            _dal.Delete(ID);
        }
    }
}
