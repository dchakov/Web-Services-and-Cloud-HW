namespace StudentSystem.Services.Models
{
    public class StudentsRequestModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Number { get; set; }

        public int Age { get; set; }

        public string Town { get; set; }

        public string Degree { get; set; }

        public string Adress { get; set; }
    }
}