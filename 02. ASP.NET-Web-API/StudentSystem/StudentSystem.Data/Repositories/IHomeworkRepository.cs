namespace StudentSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;

    public interface IHomeworkRepository
    {
        IEnumerable<Homework> GetHomeworks();
        Homework GetHomeworkByID(int homeworkId);
        void InsertHomework(Homework homework);
        void DeleteHomework(int homeworkId);
        void UpdateHomework(Homework homework);
        void Save();
    }
}
