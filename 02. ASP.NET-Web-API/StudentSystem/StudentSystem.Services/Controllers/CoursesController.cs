namespace StudentSystem.Services.Controllers
{
    using Data;
    using Data.Repositories;
    using Models;
    using System.Web.Http;

    public class CoursesController : ApiController
    {
        private ICourseRepository courseRepository;

        public CoursesController()
        {
            this.courseRepository = new CourseRepository();
        }

        public CoursesController(ICourseRepository repository)
        {
            this.courseRepository = repository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(this.courseRepository.GetCourses());
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return this.Ok(this.courseRepository.GetCourseById(id));
        }

        [HttpPost]
        public IHttpActionResult Post(CoursesRequestModel course)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var dbCourse = new Cours
            {
                Name = course.Name,
                Description = course.Description,
                Materials = course.Materials
            };
            this.courseRepository.InsertCourse(dbCourse);
            this.courseRepository.Save();

            return this.Ok(course);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CoursesRequestModel courseModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var course = this.courseRepository.GetCourseById(id);

            if (course == null)
            {
                return this.BadRequest("Invalid id");
            }
            course.Name = string.IsNullOrEmpty(courseModel.Name) ? course.Name : courseModel.Name;
            course.Description = string.IsNullOrEmpty(courseModel.Description) ? course.Description : courseModel.Description;
            course.Materials = courseModel.Materials;

            this.courseRepository.UpdateCourse(course);
            this.courseRepository.Save();
            return this.Ok(course);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var course = this.courseRepository.GetCourseById(id);

            if (course == null)
            {
                return this.BadRequest("Invalid id");
            }

            this.courseRepository.DeleteCourse(id);
            this.courseRepository.Save();
            return this.Ok();
        }
    }
}