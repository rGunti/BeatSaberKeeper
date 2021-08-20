using System.Collections.Generic;

namespace BeatSaberKeeper.Kernel.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        void Delete(T entity);
    }
}