using AutoMapper;
using StudentSystem.Api.Extensions;
using StudentSystem.Api.Models.ScheduleCourse;
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
    /// 学生选课管理
    /// </summary>
    [RoutePrefix("Api/StudentScheduleCourse")]
    public class StudentScheduleCourseController : BaseApiController
    {
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Add"), HttpPost]
        public async Task<Result> Add([FromBody]StudentScheduleCourseAddInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var userInfo = base.GetUserInfo();
                if (userInfo.UserType != UserType.Student)
                {
                    return Result.FromError("只有学生才能选课");
                }
                var student = db.Students.FirstOrDefault(x => x.UserId == userInfo.UserId);

                var studentSelectCourse = db.StudentSelectCourse.FirstOrDefault(x => x.SelectCourseId == input.SelectCourseId && student.Id == x.StudentId);
                if (studentSelectCourse != null)
                {
                    return Result.Ok();
                }
                studentSelectCourse = new StudentSelectCourse();
                studentSelectCourse.IsDeleted = false;
                studentSelectCourse.CreationTime = DateTime.Now;
                studentSelectCourse.SelectCourseId = input.SelectCourseId;
                studentSelectCourse.StudentId = student.Id;
                db.StudentSelectCourse.Add(studentSelectCourse);
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="selectCourseId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Update/{selectCourseId}"), HttpPost]
        public async Task<Result> Update(long selectCourseId)
        {
            using (var db = new ManageServerDbContext())
            {
                var userInfo = base.GetUserInfo();
                if (userInfo.UserType != UserType.Student)
                {
                    return Result.FromError("只有学生才能选课");
                }
                var student = db.Students.FirstOrDefault(x => x.UserId == userInfo.UserId);

                var studentSelectCourse = db.StudentSelectCourse.FirstOrDefault(x => selectCourseId == x.SelectCourseId && x.StudentId == student.Id);
                if (studentSelectCourse == null)
                {
                    return Result.FromError("排课不存在");
                }
                studentSelectCourse.SelectCourseId = selectCourseId;
                studentSelectCourse.ModifyTime = DateTime.Now;
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 退选
        /// </summary>
        /// <param name="selectCourseId"></param>
        /// <returns></returns>
        [Route("Delete/{selectCourseId}"), HttpGet]
        public async Task<Result> Delete(long selectCourseId)
        {
            using (var db = new ManageServerDbContext())
            {
                var userInfo = base.GetUserInfo();
                if (userInfo.UserType != UserType.Student)
                {
                    return Result.FromError("只有学生才能选课");
                }
                var student = db.Students.FirstOrDefault(x => x.UserId == userInfo.UserId);

                var studentSelectCourse = db.StudentSelectCourse.FirstOrDefault(x => selectCourseId == x.SelectCourseId && x.StudentId == student.Id);
                if (studentSelectCourse == null)
                {
                    return Result.FromError("选课不存在");
                }
                studentSelectCourse.IsDeleted = true;
                studentSelectCourse.ModifyTime = DateTime.Now;
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
        public async Task<Result> Query([FromBody]StudentScheduleCourseQueryInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var selectCourse = db.StudentSelectCourse.Where(selectCourseExp(input)).ToList();
                var pageResult = new PageResult<List<ScheduleCourseQueryOutput>>(input.CurrentPage, input.PageSize, selectCourse.Count);
                selectCourse = selectCourse.Skip((input.CurrentPage - 1) * input.PageSize).Take(input.PageSize).ToList();
                pageResult.Data = Mapper.Map<List<SelectCourse>, List<ScheduleCourseQueryOutput>>(selectCourse.Select(x => x.SelectCourse).ToList());
                return Result.Ok(pageResult);
            }
        }

        private Expression<Func<StudentSelectCourse, bool>> selectCourseExp(StudentScheduleCourseQueryInput input)
        {
            Expression<Func<StudentSelectCourse, bool>> expression = ent => true;
            var userInfo = base.GetUserInfo();
            if (userInfo.UserType == UserType.Student)
            {
                using (var db = new ManageServerDbContext())
                {
                    var student = db.Students.FirstOrDefault(x => x.UserId == userInfo.UserId);

                    expression.And(ent => ent.StudentId == student.Id);
                }
            }
            if (input.Week != null)
            {
                expression = expression.And(ent => ent.SelectCourse.Week == input.Week);
            }
            if (input.Professional != null)
            {
                expression = expression.And(ent => ent.SelectCourse.Course.Professional == input.Professional);
            }
            return expression;
        }
    }
}
