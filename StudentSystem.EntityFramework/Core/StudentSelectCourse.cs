namespace StudentSystem.EntityFramework.Core
{
    /// <summary>
    /// 学生选课表
    /// </summary>
    public class StudentSelectCourse : Entity<long>
    {
        /// <summary>
        /// 学生id
        /// </summary>
        public long StudentId { get; set; }

        /// <summary>
        /// 选课id
        /// </summary>
        public long SelectCourseId { get; set; }

        /// <summary>
        /// 选课信息
        /// </summary>
        public virtual SelectCourse SelectCourse { get; set; }

        /// <summary>
        /// 学生信息
        /// </summary>
        public virtual Students Students { get; set; }

    }
}
