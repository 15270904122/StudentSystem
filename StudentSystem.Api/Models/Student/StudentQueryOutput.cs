using StudentSystem.EntityFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentSystem.Api.Models.Student
{
    public class StudentQueryOutput : EntityOutput<long>
    {  /// <summary>
       /// 学生编号
       /// </summary>
        public string StudentNo { get; set; }

        /// <summary>
        /// 应修学分
        /// </summary>
        public int ShouldScore { get; set; }

        /// <summary>
        /// 实修学分
        /// </summary>
        public int RealScore { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public Professional Professional { get; set; }

        public long UserId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public Users Users { get; set; }
    }
}