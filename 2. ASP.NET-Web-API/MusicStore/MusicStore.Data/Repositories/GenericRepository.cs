namespace MusicStore.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MusicStoreDbContext db = null;
        private DbSet<T> table = null;

        public GenericRepository()
        {
            this.db = new MusicStoreDbContext();
            this.table = db.Set<T>();
        }

        public GenericRepository(MusicStoreDbContext db)
        {
            this.db = db;
            this.table = db.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            return table.ToList();
        }

        public T SelectById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            this.table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T exixting = this.table.Find(id);
            this.table.Remove(exixting);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
