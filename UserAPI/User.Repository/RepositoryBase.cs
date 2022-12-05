using User.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using User.Repository.Interfaces;
using System.Linq;
using System;

namespace User.Repository

{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected UserDBContext UserDbContext { get; set; }
        public RepositoryBase(UserDBContext userDBContext)
        {
            this.UserDbContext = userDBContext;
        }

        public IQueryable<T> FindAll() => UserDbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            UserDbContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => UserDbContext.Set<T>().Add(entity);

        public void Update(T entity) => UserDbContext.Set<T>().Update(entity);

        public void Delete(T entity) => UserDbContext.Set<T>().Remove(entity);
    }
}