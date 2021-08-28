using System;
using System.Collections.Generic;

namespace BeatSaberKeeper.Kernel.Repositories
{
    [Obsolete]
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        void Delete(T entity);
    }
}