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

                var selectCourse = db.SelectCourse.FirstOrDefault(x => x.Id == input.SelectCourseId);
                var courseEntity = db.Course.FirstOrDefault(x => x.Id == selectCourse.CourseId);
                var courseCount = db.SelectCourse.Where(x => x.CourseId == courseEntity.Id).Count();
                if (courseCount >= courseEntity.PersonLimit)
                {
                    return Result.FromError("人数已上限");
                }
                studentSelectCourse = new StudentSelectCourse();
                studentSelectCourse.IsDeleted = false;
                studentSelectCourse.CreationTime = DateTime.Now;
                studentSelectCourse.SelectCourseId = input.SelectCourseId;
                studentSelectCourse.StudentId = student.Id;
                student.RealScore += courseEntity.Score;
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
        [Route("Update"), HttpPost]
        public async Task<Result> Update(StudentScheduleCourseUpdateInput studentScheduleCourseUpdateInput)
        {
            using (var db = new ManageServerDbContext())
            {
                var userInfo = base.GetUserInfo();
                if (userInfo.UserType != UserType.Student)
                {
                    return Result.FromError("只有学生才能选课");
                }
                var student = db.Students.FirstOrDefault(x => x.UserId == userInfo.UserId);

                var studentSelectCourse = db.StudentSelectCourse.FirstOrDefault(x => studentScheduleCourseUpdateInput.OldSelectCourseId == x.SelectCourseId && x.StudentId == student.Id);
                if (studentSelectCourse == null)
                {
                    return Result.FromError("排课不存在");
                }

                var courseEntity = db.SelectCourse.FirstOrDefault(x => x.Id == studentScheduleCourseUpdateInput.OldSelectCourseId).Course;
                var courseCount = db.SelectCourse.Where(x => x.CourseId == courseEntity.Id).Count();
                if (courseCount >= courseEntity.PersonLimit)
                {
                    return Result.FromError("人数已上限");
                }

                studentSelectCourse.SelectCourseId = studentScheduleCourseUpdateInput.NewSelectCourseId;
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
                student.RealScore -= studentSelectCourse.SelectCourse.Course.Score;
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

        /// <summary>
        /// 选修套餐
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("SelectSetMeal"), HttpGet]
        public async Task<Result> SelectSetMeal()
        {
            //获取当前学生选课信息，学分差数，
            //获取当前学生专业、学分排序、时间不冲突的前几位（学分之和大于学分差值）
            using (var db = new ManageServerDbContext())
            {
                var userInfo = base.GetUserInfo();
                if (userInfo.UserType != UserType.Student)
                {
                    return Result.FromError("只有学生才有选修套餐");
                }
                var student = db.Students.FirstOrDefault(x => x.UserId == userInfo.UserId);

                var studentSelectCourse = db.StudentSelectCourse.Where(x => x.StudentId == student.Id).ToList();
                var currentScore = student.RealScore;
                var selectCourseId = studentSelectCourse.Select(x => x.SelectCourseId).ToList();
                var selectCourseEntities = db.SelectCourse.Where(x => x.Course.Professional == student.Professional && !selectCourseId.Contains(x.Id)).OrderByDescending(x => x.Course.Score).ToList();
                return Result.Ok(Mapper.Map<List<SelectCourse>, List<ScheduleCourseQueryOutput>>(selectCourseEntities));
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
