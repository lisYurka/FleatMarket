using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleatMarket.Base.Interfaces
{
    public interface IBaseRepository:IDisposable
    {
        IEnumerable<T> GetAll<T>() where T:class;
        IEnumerable<T> GetWithInclude<T>(params string[] query) where T : class;
        T GetWithIncludeById<T>(string id, params string[] _query) where T : class;
        T GetById<T>(int id) where T : class;
        void Save();
        void Create<T>(T item) where T : class;
        void Update<T>(T item) where T : class;
        void Remove<T>(T item) where T : class;
        T GetByStringId<T>(string id) where T:class;


        Task CreateAsync<T>(T item) where T : class;
        Task<int> SaveAsync();
    }
}
