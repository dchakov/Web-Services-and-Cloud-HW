namespace StudentSystem.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class CourseRepository : ICourseRepository
    {
        private StudentSystemEntities db;

        public CourseRepository()
        {
            this.db = new StudentSystemEntities();
        }

        public CourseRepository(StudentSystemEntities db)
        {
            this.db = db;
        }

        public IEnumerable<Cours> GetCourses()
        {
            return this.db.Courses.ToList();
        }

        public Cours GetCourseById(int courseId)
        {
            return this.db.Courses.Find(courseId);
        }

        public void InsertCourse(Cours course)
        {
            this.db.Courses.Add(course);
        }
        
        public void UpdateCourse(Cours course)
        {
            this.db.Entry(course).State = EntityState.Modified;
        }

        public void DeleteCourse(int courseId)
        {
            this.db.Courses.Remove(this.db.Courses.Find(courseId));
        }

        public void Save()
        {
            this.db.SaveChanges();
        }
    }
}
