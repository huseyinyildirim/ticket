using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Data.Model;
using PagedList;

namespace Data.DAL
{
    public class Base<T> where T : class
    {
        public DBDataContext DB = new DBDataContext();

        public List<T> Get()
        {
            var q = DB.Set<T>();
            return q.ToList();
        }

        public List<T> Get(Expression<Func<T, bool>> where)
        {
            var q = DB.Set<T>().Where(where);
            return q.ToList();
        }
        public bool GetAny(Expression<Func<T, bool>> where)
        {
            var q = DB.Set<T>().Any(where);
            return q;
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> where)
        {
            var q = DB.Set<T>().FirstOrDefault(where);
            return q;
        }
        public int GetCount(Expression<Func<T, bool>> where)
        {
            var q = DB.Set<T>().Count(where);
            return q;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return DB.Set<T>().FirstOrDefault(where);
        }

        public List<T> Get(Expression<Func<T, bool>> where, int P, int pageSize)
        {
            P = (P == 0 ? 1 : P);
            pageSize = (pageSize == 0 ? 15 : pageSize);
            var q = DB.Set<T>().OrderBy("ID").Where(where).ToPagedList(P, pageSize);
            return q.ToList();
        }

        public void Add(T entity)
        {
            try
            {
                var status = entity.GetType().GetProperty("STATUS");
                status.SetValue(entity, true, null);
            }
            catch
            {
            }

            try
            {
            DB.Set<T>().Add(entity);
                DB.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

        public void Update(T entity)
        {
            DB.Entry(entity).State = EntityState.Modified;
            DB.SaveChanges();
        }

        public void Delete(int ID)
        {
            var entity = DB.Set<T>().Find(ID);
            try
            {
                var status = entity.GetType().GetProperty("STATUS");
                status.SetValue(entity, false, null);
            }
            catch
            {
            }

            DB.Entry(entity).State = EntityState.Modified;
            DB.SaveChanges();
        }

        public void Delete(int ID, bool realDelete)
        {
            var entity = DB.Set<T>().Find(ID);
            DB.Entry(entity).State = EntityState.Deleted;
            DB.SaveChanges();

        }


        public Func<T, Object> GetExpression<T>(String sortby)
        {
            var param = Expression.Parameter(typeof(T));
            var sortExpression = Expression.Lambda<Func<T, Object>>(Expression.Convert(Expression.Property(param, sortby), typeof(Object)), param);
            return sortExpression.Compile();
        }

        public void Dispose()
        {
            if (DB != null) DB.Dispose();
        }
    }
}