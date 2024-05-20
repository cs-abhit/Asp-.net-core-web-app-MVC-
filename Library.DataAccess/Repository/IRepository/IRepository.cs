using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class   // we didn't know which class implemented so use generic class.
    {
        //T - category

        IEnumerable<T> GetAll();
        T GetValue(Expression<Func<T,bool>> filter);
        void Add(T entity);

        // void Update(T entity);       // we have to update different filed so it is easy to direct update instead of repository interface.
        
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
