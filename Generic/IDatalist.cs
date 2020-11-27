using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ToDoApp_v1._2.Generic
{
    public interface IDatalist<T> where T : class
    {
        T GetById(int Id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
