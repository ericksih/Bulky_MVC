using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository <T> where T : class
    {
        // T - Category
        IEnumerable<T> GetAll (string? includeProperties = null);
        T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        //void Update(T entity); -> keep the update do in controller cause in some case update is just need to updated some property.
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
