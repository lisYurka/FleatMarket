using System;
using System.Collections.Generic;
using System.Text;

namespace FleatMarket.Model.Interface
{
    public interface IBaseRepository:IDisposable
    {
        IEnumerable<T> GetAll<T>() where T:class;
        IEnumerable<T> GetWithInclude<T>(params string[] query) where T : class;
        T GetWithIncludeById<T>(int id, params string[] _query) where T : class;
        T GetById<T>(int id) where T : class;
        void Save();
        void Create<T>(T item) where T : class;
        void Update<T>(T item) where T : class;
        void Remove<T>(T item) where T : class;
    }
}
