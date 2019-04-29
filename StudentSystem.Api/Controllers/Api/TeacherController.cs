using AutoMapper;
using StudentSystem.Api.Extensions;
using StudentSystem.Api.Models.Teacher;
using StudentSystem.EntityFramework;
using StudentSystem.EntityFramework.Core;
using StudentSystem.Infrastructure.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentSystem.Api.Controllers.Api
{
    /// <summary>
    /// 教师管理
    /// </summary>
    [RoutePrefix("Api/Teacher")]
    public class TeacherController : BaseApiController
    {
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Add"), HttpPost]
        public async Task<Result> Add([FromBody]AddInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var user = db.Teachers.FirstOrDefault(x => x.Users.UserType == UserType.Teacher && x.Users.UserName == input.UserName || x.TeacherNo == input.TeacherNo);
                if (user != null)
                {
                    return Result.FromError("用户已存在或教师编号已存在");
                }
                var teacher = new Teachers();
                teacher.IsDeleted = false;
                teacher.CreationTime = DateTime.Now;
                teacher.TeacherNo = input.TeacherNo;
                teacher.TeacherRank = input.TeacherRank;
                teacher.Professional = input.Professional;
                teacher.Users = new Users();
                teacher.Users.CreationTime = DateTime.Now;
                teacher.Users.Email = input.Email;
                teacher.Users.Gender = input.Gender;
                teacher.Users.IsDeleted = false;
                teacher.Users.UserName = input.UserName;
                teacher.Users.Phone = input.Phone;
                teacher.Users.Password = input.Password;
                teacher.Users.Name = input.Name;
                teacher.Users.IdCard = input.IdCard;
                teacher.Users.UserType = UserType.Teacher;
                db.Teachers.Add(teacher);
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Update/{teacherId}"), HttpPost]
        public async Task<Result> Update(long teacherId, [FromBody]UpdateInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var teacher = db.Teachers.FirstOrDefault(x => teacherId == x.Id);
                if (teacher == null)
                {
                    return Result.FromError("教师不存在");
                }
                teacher.TeacherRank = input.TeacherRank;
                teacher.Professional = input.Professional;
                teacher.Users.Email = input.Email;
                teacher.ModifyTime = DateTime.Now;
                teacher.Users.Phone = input.Phone;
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [Route("Delete/{teacherId}"), HttpGet]
        public async Task<Result> Delete(long teacherId)
        {
            using (var db = new ManageServerDbContext())
            {
                var teacher = db.Teachers.FirstOrDefault(x => teacherId == x.Id);
                if (teacher == null)
                {
                    return Result.FromError("学生不存在或已删除");
                }
                var selectCourse = db.SelectCourse.FirstOrDefault(x => x.TeacherId == teacherId);
                if (selectCourse != null)
                {
                    return Result.FromError("教师有排课不能删除");
                }
                teacher.IsDeleted = true;
                teacher.ModifyTime = DateTime.Now;
                teacher.Users.IsDeleted = true;
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Query"), HttpPost]
        public async Task<Result> Query([FromBody]QueryInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var teachers = db.Teachers.Where(TeachersExp(input)).ToList();

                return Result.Ok(Mapper.Map<List<Teachers>, List<QueryOutput>>(teachers));

            }
        }

        private Expression<Func<Teachers, bool>> TeachersExp(QueryInput input)
        {
            Expression<Func<Teachers, bool>> expression = ent => true;
            if (!string.IsNullOrEmpty(input.TeacherNo))
            {
                expression = expression.And(ent => ent.TeacherNo == input.TeacherNo);
            }
            if (!string.IsNullOrEmpty(input.Name))
            {
                expression = expression.And(ent => ent.Users.Name.Contains(input.Name));
            }
            if (input.Professional != null)
            {
                expression = expression.And(ent => ent.Professional == input.Professional);
            }
            return expression;
        }
    }
}
