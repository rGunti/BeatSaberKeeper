using System.Collections.Generic;

namespace BeatSaberKeeper.Kernel.Abstraction.Repo
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        bool Exists(string id);
        void Clone(T entity, string newId);
        void Rename(T entity, string newId);
        void Delete(T entity);
    }
}