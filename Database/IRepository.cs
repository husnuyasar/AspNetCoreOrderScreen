using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderScreen.Data;

namespace OrderScreen.Database
{
    public interface IRepository<T> where T : Entity
    {
        DbSet<T> Table { get; }
        int Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);

        List<T> All();
        T GetById(int id);
        bool Any(Expression<Func<T, bool>> where);
        IQueryable<T> Where(Expression<Func<T, bool>> where);
        IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc);
    }
}