using ComputerInventory.Core.Repositories;
using ComputerInventory.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInventory.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ComputerInventoryContext _context { get; }
        protected DbSet<T> _dbSet => _context.Set<T>();

        public RepositoryBase(ComputerInventoryContext context)
        {
            _context = context;
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
           return _dbSet;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public void Update(T entity)
        {
           _dbSet.Update(entity);
        }
    }
}
