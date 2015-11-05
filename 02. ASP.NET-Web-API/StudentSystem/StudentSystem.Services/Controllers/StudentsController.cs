namespace StudentSystem.Services.Controllers
{
    using Data;
    using Data.Repositories;
    using Models;
    using System.Web.Http;

    public class StudentsController : ApiController
    {
        private IStudentRepository studentRepository;

        public StudentsController()
        {
            this.studentRepository = new StudentRepository();
        }

        public StudentsController(IStudentRepository repository)
        {
            this.studentRepository = repository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var students = this.studentRepository.GetStudents();
            return this.Ok(students);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var student = this.studentRepository.GetStudentByID(id);
            return this.Ok(student);
        }

        [HttpPost]
        public IHttpActionResult Post(StudentsRequestModel student)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var dbStudent = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age
            };
            this.studentRepository.InsertStudent(dbStudent);
            this.studentRepository.Save();

            return this.Ok(student);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]StudentsRequestModel studentModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var student = this.studentRepository.GetStudentByID(id);

            if (student == null)
            {
                return this.BadRequest("Invalid id");
            }
            student.FirstName = string.IsNullOrEmpty(studentModel.FirstName) ? student.FirstName : studentModel.FirstName;
            student.LastName = string.IsNullOrEmpty(studentModel.LastName)? student.LastName: studentModel.LastName;
            student.Number = studentModel.Number;
            student.Age = studentModel.Age;
            student.Town = studentModel.Town;
            student.Adress = studentModel.Adress;
            student.Degree = studentModel.Degree;

            this.studentRepository.UpdateStudent(student);
            this.studentRepository.Save();
            return this.Ok(student);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var student = this.studentRepository.GetStudentByID(id);

            if (student == null)
            {
                return this.BadRequest("Invalid id");
            }

            this.studentRepository.DeleteStudent(id);
            this.studentRepository.Save();
            return this.Ok();
        }
    }
}