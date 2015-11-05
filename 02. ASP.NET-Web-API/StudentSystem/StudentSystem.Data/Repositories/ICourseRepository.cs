namespace StudentSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;

    public interface ICourseRepository
    {
        IEnumerable<Cours> GetCourses();
        Cours GetCourseById(int courseId);
        void InsertCourse(Cours course);
        void DeleteCourse(int courseId);
        void UpdateCourse(Cours course);
        void Save();
    }
}
