using AutoMapper;
using StudentSystem.Api.Extensions;
using StudentSystem.Api.Models.Course;
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
    /// 课程管理
    /// </summary>
    [RoutePrefix("Api/Course")]
    public class CourseController : BaseApiController
    {
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Add"), HttpPost]
        public async Task<Result> Add([FromBody]CourseAddInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var user = db.Course.FirstOrDefault(x => x.CourseName == input.CourseName);
                if (user != null)
                {
                    return Result.FromError("课程已存在");
                }
                var course = new Course();
                course.IsDeleted = false;
                course.CreationTime = DateTime.Now;
                course.CourseName = input.CourseName;
                course.Professional = input.Professional;
                course.Score = input.Score;
                course.PersonLimit = input.PersonLimit;
                course.Introduction = input.Introduction;
                db.Course.Add(course);
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Update/{courseId}"), HttpPost]
        public async Task<Result> Update(long courseId, [FromBody]CourseUpdateInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var course = db.Course.FirstOrDefault(x => courseId == x.Id);
                if (course == null)
                {
                    return Result.FromError("课程不存在");
                }
                course.CourseName = input.CourseName;
                course.Professional = input.Professional;
                course.Score = input.Score;
                course.Introduction = input.Introduction;
                course.PersonLimit = input.PersonLimit;
                course.ModifyTime = DateTime.Now;
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [Route("Delete/{courseId}"), HttpGet]
        public async Task<Result> Delete(long courseId)
        {
            using (var db = new ManageServerDbContext())
            {
                var course = db.Course.FirstOrDefault(x => courseId == x.Id);
                if (course == null)
                {
                    return Result.FromError("课程不存在或已删除");
                }
                var selectCourse = db.SelectCourse.FirstOrDefault(x => x.CourseId == courseId);
                if (selectCourse != null)
                {
                    return Result.FromError("有排课不能删除");
                }
                selectCourse.IsDeleted = true;
                course.ModifyTime = DateTime.Now;
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
        public async Task<Result> Query([FromBody]CourseQueryInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var courses = db.Course.Where(CourseExp(input)).ToList();
                var pageResult = new PageResult<List<CourseQueryOutput>>(input.CurrentPage, input.PageSize, courses.Count);
                courses = courses.Skip((input.CurrentPage - 1) * input.PageSize).Take(input.PageSize).ToList();
                pageResult.Data = Mapper.Map<List<Course>, List<CourseQueryOutput>>(courses);
                return Result.Ok(pageResult);
            }
        }

        private Expression<Func<Course, bool>> CourseExp(CourseQueryInput input)
        {
            Expression<Func<Course, bool>> expression = ent => true;

            if (!string.IsNullOrEmpty(input.CourseName))
            {
                expression = expression.And(ent => ent.CourseName.Contains(input.CourseName));
            }
            if (input.Professional != null)
            {
                expression = expression.And(ent => ent.Professional == input.Professional);
            }
            return expression;
        }
    }
}
