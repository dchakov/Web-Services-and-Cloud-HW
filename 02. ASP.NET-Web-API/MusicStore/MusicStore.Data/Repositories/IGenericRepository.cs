namespace MusicStore.Data.Repositories
{
    using System.Collections.Generic;

    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void SaveChanges();
    }
}
