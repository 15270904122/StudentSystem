using StudentSystem.EntityFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentSystem.Api.Models.Student
{
    public class QueryInput
    {
        /// <summary>
        /// 学生编号
        /// </summary>
        public string StudentNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}