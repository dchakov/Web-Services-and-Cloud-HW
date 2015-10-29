namespace StudentSystem.Services.Models
{
    using System;

    public class HomeworkRequestModels
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }
    }
}