namespace StudentSystem.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class StudentRepository : IStudentRepository
    {
        private StudentSystemEntities db;

        public StudentRepository()
        {
            this.db = new StudentSystemEntities();
        }

        public StudentRepository(StudentSystemEntities db)
        {
            this.db = db;
        }

        public IEnumerable<Student> GetStudents()
        {
            return this.db.Students.ToList();
        }

        public Student GetStudentByID(int studentId)
        {
            return this.db.Students.Find(studentId);
        }

        public void InsertStudent(Student student)
        {
            this.db.Students.Add(student);
        }

        public void DeleteStudent(int studentID)
        {
            Student student = db.Students.Find(studentID);
            db.Students.Remove(student);
        }

        public void UpdateStudent(Student student)
        {
            db.Entry(student).State = EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
