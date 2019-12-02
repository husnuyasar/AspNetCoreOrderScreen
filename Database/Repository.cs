using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderScreen.Data;

namespace OrderScreen.Database
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly CoreDbContext dbContext;
        public Repository(CoreDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.Table = dbContext.Set<T>();
        }
        public DbSet<T> Table { get; set; }

        public int Add(T entity)
        {
            try
            {
                 Table.Add(entity);
            Save();
            return entity.id;
            }
            catch (System.Exception ex)
            {
                
                throw;
            }   
           
        }

        public bool Update(T entity)
        {
            Table.Update(entity);
            return Save();
        }

        public bool Delete(T entity)
        {
            Table.Remove(entity);
            return Save();
        }

        public List<T> All()
        {
            return Table.ToList();
        }

        public T GetById(int id)
        {
            return Table.FirstOrDefault(e => e.id == id);
        }
        /*public List<T> Sort()
        {
            return Table.OrderBy(x=> x.created_at).ToList();
        }*/
        public bool Any(Expression<Func<T, bool>> where)
        {
            return Table.Any(where);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return Table.Where(where);
        }

        public IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc)
        {
            if (isDesc)
                return Table.OrderByDescending(orderBy);
            return Table.OrderBy(orderBy);
        }


        private bool Save()
        {
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                // TODO: Log Exceptions
                return false;
            }
        }

    }
}