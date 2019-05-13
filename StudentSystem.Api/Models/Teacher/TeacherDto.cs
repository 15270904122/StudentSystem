using StudentSystem.Api.Map;
using StudentSystem.Api.Models.Auth;
using StudentSystem.EntityFramework.Core;
using StudentSystem.Infrastructure.Extensions;

namespace StudentSystem.Api.Models.Teacher
{
    [AutoMapFrom]
    public class TeacherDto : EntityOutput<long>
    {
        /// <summary>
        /// 教师编号
        /// </summary>
        public string TeacherNo { get; set; }

        /// <summary>
        /// 教师级别
        /// </summary>
        public TeacherRank TeacherRank { get; set; }
        /// <summary>
        /// 教师级别
        /// </summary>
        public string TeacherRankName => TeacherRank.DisplayName();
        /// <summary>
        /// 专业
        /// </summary>
        public Professional Professional { get; set; }
        public string ProfessionalName => Professional.DisplayName();

        public long UserId { get; set; }

        public UserDto Users { get; set; }

    }
}
