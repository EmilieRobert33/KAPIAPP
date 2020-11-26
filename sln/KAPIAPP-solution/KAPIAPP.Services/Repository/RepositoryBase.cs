using KAPIAPP.Services.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KAPIAPP.Services.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationContext _applicationContext;

        public RepositoryBase(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IQueryable<T> FindAll()
        {
            return _applicationContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _applicationContext.Set<T>().Where(expression).AsNoTracking();
        }        

        public void Create(T entity)
        {
            _applicationContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _applicationContext.Set<T>().Remove(entity);
        }
        
        public void Update(T entity)
        {
            _applicationContext.Set<T>().Update(entity);
        }
    }
}
