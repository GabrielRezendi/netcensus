using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCensus.Manager.DAL.Context;

namespace NetCensus.Manager.DAL.DAO
{
    public class BaseDAO<T> : IBaseDAO<T> where T : class
    {
        private ManagerContext context;
        public BaseDAO()
        {
            context = new ManagerContext();
        }

        public virtual void Add(T entity)
        {
            using (context = new ManagerContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }

        }

        public virtual List<T> Get()
        {
            return context.Set<T>().ToList();
        }

        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            using (context = new ManagerContext())
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (context = new ManagerContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
