using AutoMapper;
using StudentSystem.Api.Extensions;
using StudentSystem.Api.Models.Auth;
using StudentSystem.Api.Models.Student;
using StudentSystem.EntityFramework;
using StudentSystem.EntityFramework.Core;
using StudentSystem.Infrastructure.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Http;

namespace StudentSystem.Api.Controllers.Api
{
    /// <summary>
    /// 学生管理
    /// </summary>
    [RoutePrefix("Api/Student")]
    public class StudentController : BaseApiController
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
                var user = db.Students.FirstOrDefault(x => x.Users.UserType == UserType.Student && x.Users.UserName == input.UserName || x.StudentNo == input.StudentNo);
                if (user != null)
                {
                    return Result.FromError("用户已存在或学生编号已存在");
                }
                var student = new Students();
                student.IsDeleted = false;
                student.CreationTime = DateTime.Now;
                student.StudentNo = input.StudentNo;
                student.ShouldScore = input.ShouldScore;
                student.RealScore = 0;
                student.Professional = input.Professional;
                student.Users = new Users();
                student.Users.CreationTime = DateTime.Now;
                student.Users.Email = input.Email;
                student.Users.Gender = input.Gender;
                student.Users.IsDeleted = false;
                student.Users.UserName = input.UserName;
                student.Users.Phone = input.Phone;
                student.Users.Password = input.Password;
                student.Users.Name = input.Name;
                student.Users.IdCard = input.IdCard;
                student.Users.UserType = UserType.Student;
                db.Students.Add(student);
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Update/{studentId}"), HttpPost]
        public async Task<Result> Update(long studentId, [FromBody]UpdateInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var student = db.Students.FirstOrDefault(x => studentId == x.Id);
                if (student == null)
                {
                    return Result.FromError("学生不存在");
                }
                student.StudentNo = input.StudentNo;
                student.ShouldScore = input.ShouldScore;
                student.Professional = input.Professional;
                student.Users.Email = input.Email;
                student.Users.Gender = input.Gender;
                student.Users.UserName = input.UserName;
                student.Users.Phone = input.Phone;
                student.Users.Password = input.Password;
                student.Users.Name = input.Name;
                student.Users.IdCard = input.IdCard;
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [Route("Delete/{studentId}"), HttpGet]
        public async Task<Result> Delete(long studentId)
        {
            using (var db = new ManageServerDbContext())
            {
                var student = db.Students.FirstOrDefault(x => studentId == x.Id);
                if (student == null)
                {
                    return Result.FromError("学生不存在或已删除");
                }
                student.IsDeleted = true;
                student.Users.IsDeleted = true;
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
                var students = db.Students.Where(StudentsExp(input)).ToList();

                return Result.Ok(Mapper.Map<List<Students>, List<QueryOutput>>(students));

            }
        }

        private Expression<Func<Students, bool>> StudentsExp(QueryInput input)
        {
            Expression<Func<Students, bool>> expression = ent => true;
            if (!string.IsNullOrEmpty(input.StudentNo))
            {
                expression = expression.And(ent => ent.StudentNo == input.StudentNo);
            }
            if (!string.IsNullOrEmpty(input.Name))
            {
                expression = expression.And(ent => ent.Users.Name.Contains(input.Name));
            }
            return expression;
        }
    }
}
