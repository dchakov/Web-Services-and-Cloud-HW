namespace StudentSystem.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class HomeworkRepository : IHomeworkRepository
    {
        private StudentSystemEntities db;

        public HomeworkRepository()
        {
            this.db = new StudentSystemEntities();
        }

        public HomeworkRepository(StudentSystemEntities db)
        {
            this.db = db;
        }

        public IEnumerable<Homework> GetHomeworks()
        {
            return this.db.Homework.ToList();
        }

        public Homework GetHomeworkByID(int homeworkId)
        {
            return this.db.Homework.Find(homeworkId);
        }

        public void InsertHomework(Homework homework)
        {
            this.db.Homework.Add(homework);
        }

        public void DeleteHomework(int homeworkId)
        {
            this.db.Homework.Remove(this.db.Homework.Find(homeworkId));
        }

        public void UpdateHomework(Homework homework)
        {
            this.db.Entry(homework).State = EntityState.Modified;
        }

        public void Save()
        {
            this.db.SaveChanges();
        }
    }
}
