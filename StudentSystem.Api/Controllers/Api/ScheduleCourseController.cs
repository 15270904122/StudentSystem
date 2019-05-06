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
    /// 排课管理
    /// </summary>
    [RoutePrefix("Api/ScheduleCourse")]
    public class ScheduleCourseController : BaseApiController
    {
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Add"), HttpPost]
        public async Task<Result> Add([FromBody]ScheduleCourseAddInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var user = db.SelectCourse.FirstOrDefault(x => x.CouresId == input.CouresId && x.Week == input.Week && x.Time == input.Time);
                if (user != null)
                {
                    return Result.FromError("课程已排课");
                }

                var selectCourse = new SelectCourse();
                selectCourse.IsDeleted = false;
                selectCourse.CreationTime = DateTime.Now;
                selectCourse.CouresId = input.CouresId;
                selectCourse.IsActive = input.IsActive;
                selectCourse.Time = input.Time;
                selectCourse.TeacherId = input.TeacherId;
                selectCourse.Week = input.Week;
                db.SelectCourse.Add(selectCourse);
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
        public async Task<Result> Update(long selectCourseId, [FromBody]ScheduleCourseUpdateInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var selectCourse = db.SelectCourse.FirstOrDefault(x => selectCourseId == x.Id);
                if (selectCourse == null)
                {
                    return Result.FromError("排课不存在");
                }
                selectCourse.CouresId = input.CouresId;
                selectCourse.IsActive = input.IsActive;
                selectCourse.Time = input.Time;
                selectCourse.TeacherId = input.TeacherId;
                selectCourse.Week = input.Week;
                selectCourse.ModifyTime = DateTime.Now;
                await db.SaveChangesAsync();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="selectCourseId"></param>
        /// <returns></returns>
        [Route("Delete/{selectCourseId}"), HttpGet]
        public async Task<Result> Delete(long selectCourseId)
        {
            using (var db = new ManageServerDbContext())
            {
                var selectCourse = db.SelectCourse.FirstOrDefault(x => selectCourseId == x.Id);
                if (selectCourse == null)
                {
                    return Result.FromError("排课不存在或已删除");
                }
                var studentSelectCourse = db.StudentSelectCourse.FirstOrDefault(x => x.SelectCourseId == selectCourseId);
                if (studentSelectCourse != null)
                {
                    return Result.FromError("有学生选课不能删除");
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
        public async Task<Result> Query([FromBody]ScheduleCourseQueryInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var selectCourse = db.SelectCourse.Where(selectCourseExp(input)).ToList();
                var pageResult = new PageResult<List<ScheduleCourseQueryOutput>>(input.CurrentPage, input.PageSize, selectCourse.Count);
                selectCourse = selectCourse.Skip((input.CurrentPage - 1) * input.PageSize).Take(input.PageSize).ToList();
                pageResult.Data = Mapper.Map<List<SelectCourse>, List<ScheduleCourseQueryOutput>>(selectCourse);
                return Result.Ok(pageResult);
            }
        }

        private Expression<Func<SelectCourse, bool>> selectCourseExp(ScheduleCourseQueryInput input)
        {
            Expression<Func<SelectCourse, bool>> expression = ent => true;

            var userInfo = base.GetUserInfo();
            if (userInfo.UserType == UserType.Teacher)
            {
                using (var db = new ManageServerDbContext())
                {
                    var teacher = db.Teachers.FirstOrDefault(x => x.UserId == userInfo.UserId);

                    expression.And(ent => ent.TeacherId == teacher.Id);
                }
            }
            if (input.Week != null)
            {
                expression = expression.And(ent => ent.Week == input.Week);
            }
            if (input.Professional != null)
            {
                expression = expression.And(ent => ent.Course.Professional == input.Professional);
            }
            return expression;
        }
    }
}
