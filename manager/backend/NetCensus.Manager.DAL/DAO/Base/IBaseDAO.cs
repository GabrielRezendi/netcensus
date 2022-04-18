using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCensus.Manager.DAL.DAO
{
    public interface IBaseDAO<T> 
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        List<T> Get();
        T Get(int id);
    }
}
