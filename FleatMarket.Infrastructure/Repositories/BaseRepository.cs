using FleatMarket.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DbContext dbContext;
        private bool isDisposed = false;

        public BaseRepository(DbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return dbContext.Set<T>().ToList();
        }

        public IEnumerable<T> GetWithInclude<T>(params string[] _query) where T : class
        {
            var query = dbContext.Set<T>();
            foreach (var item in _query)
                query.Include(item).ToList();
            return query;
        }

        public T GetWithIncludeByStringId<T>(string id, params string[] _query) where T : class
        {
            var allItems = dbContext.Set<T>();
            foreach (var item in _query)
                allItems.Include(item).ToList();
            T result = allItems.Find(id);
            return result;
        }

        public T GetWithIncludeById<T>(int id, params string[] _query) where T : class
        {
            var allItems = dbContext.Set<T>();
            foreach (var item in _query)
                allItems.Include(item).ToList();
            T result = allItems.Find(id);
            return result;
        }

        public T GetById<T>(int id) where T : class
        {
            return dbContext.Find<T>(id);
        }

        public T GetByStringId<T>(string id) where T : class
        {
            return dbContext.Find<T>(id);
        }

        public void Create<T>(T item) where T : class
        {
            if (item != null)
            {
                dbContext.Add<T>(item);
                Save();
            }
            else
                throw new Exception("Item can't be null!");
        }

        public void Update<T>(T item) where T : class
        {
            if (item != null)
            {
                dbContext.Entry(item).State = EntityState.Modified;
                Save();
            }
            else
                throw new Exception("Item can't be null!");
        }

        public void Remove<T>(T item) where T : class
        {
            if (item != null)
            {
                dbContext.Remove<T>(item);
                Save();
            }
            else
                throw new Exception("Item can't be null!");
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool flag)
        {
            if (!this.isDisposed)
                if (flag)
                    dbContext.Dispose();
            isDisposed = true;
        }



        public Task CreateAsync<T>(T item) where T : class
        {
            if (item != null)
            {
                dbContext.Add<T>(item);
                return SaveAsync();
            }
            else
                throw new Exception("Item can't be null!");
        }

        public Task<int> SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
