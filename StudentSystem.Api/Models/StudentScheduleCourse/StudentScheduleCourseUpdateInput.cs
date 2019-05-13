namespace StudentSystem.Api.Models.ScheduleCourse
{
    public class StudentScheduleCourseUpdateInput
    {
        /// <summary>
        /// 选课id
        /// </summary>
        public long OldSelectCourseId { get; set; }

        /// <summary>
        /// 选课id
        /// </summary>
        public long NewSelectCourseId { get; set; }
    }
}
