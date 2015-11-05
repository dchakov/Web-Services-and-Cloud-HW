namespace StudentSystem.Services.Controllers
{
    using Data;
    using Data.Repositories;
    using Models;
    using System.Web.Http;

    public class HomeworksController: ApiController
    {
        private IHomeworkRepository homeworkRepository;

        public HomeworksController()
        {
            this.homeworkRepository = new HomeworkRepository();
        }

        public HomeworksController(IHomeworkRepository repository)
        {
            this.homeworkRepository = repository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(this.homeworkRepository.GetHomeworks());
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return this.Ok(this.homeworkRepository.GetHomeworkByID(id));
        }

        [HttpPost]
        public IHttpActionResult Post(HomeworkRequestModels homework)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var dbHomework = new Homework
            {
                Content = homework.Content,
                TimeSent = homework.TimeSent,
                StudentId = homework.StudentId,
                CourseId = homework.CourseId
            };
            this.homeworkRepository.InsertHomework(dbHomework);
            this.homeworkRepository.Save();

            return this.Ok(homework);
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]HomeworkRequestModels homeworkModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var homework = this.homeworkRepository.GetHomeworkByID(id);

            if (homework == null)
            {
                return this.BadRequest("Invalid id");
            }
            homework.Content = string.IsNullOrEmpty(homeworkModel.Content) ? homework.Content : homeworkModel.Content;
            homework.TimeSent = homeworkModel.TimeSent;
            homework.StudentId = homeworkModel.StudentId;
            homework.CourseId = homeworkModel.StudentId;

            this.homeworkRepository.UpdateHomework(homework);
            this.homeworkRepository.Save();
            return this.Ok(homework);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var course = this.homeworkRepository.GetHomeworkByID(id);

            if (course == null)
            {
                return this.BadRequest("Invalid id");
            }

            this.homeworkRepository.DeleteHomework(id);
            this.homeworkRepository.Save();
            return this.Ok();
        }
    }
}