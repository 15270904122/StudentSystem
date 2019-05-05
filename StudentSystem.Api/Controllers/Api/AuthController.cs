using StudentSystem.Api.Models.Auth;
using StudentSystem.EntityFramework;
using StudentSystem.Infrastructure.Result;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Http;

namespace StudentSystem.Api.Controllers.Api
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [RoutePrefix("Api/Auth")]
    public class AuthController : BaseApiController
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("Login"), HttpPost, AllowAnonymous]
        public async Task<Result> Login([FromBody]LoginInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var user = db.Users.AsNoTracking().SingleOrDefault(x => x.Password == input.Password && x.UserName == input.UserName && x.UserType == input.UserType);
                if (user != null)
                {
                    var cache = new Cache();
                    var token = Guid.NewGuid().ToString("N");
                    var userInfo = new UserInfo();
                    userInfo.UserType = user.UserType;
                    userInfo.Name = user.Name;
                    userInfo.UserId = user.Id;
                    userInfo.UserName = user.UserName;
                    cache.Insert(token, userInfo);

                    LoginOutput loginOutput = new LoginOutput();

                    if (input.UserType == EntityFramework.Core.UserType.Student)
                    {
                        var student = db.Students.FirstOrDefault(x => x.UserId == user.Id);
                        loginOutput.UserNo = student.StudentNo;
                        loginOutput.Professional = student.Professional;
                        loginOutput.ShouldScore = student.ShouldScore;
                        loginOutput.RealScore = student.RealScore;
                    }
                    else if (input.UserType == EntityFramework.Core.UserType.Teacher)
                    {
                        var teacher = db.Teachers.FirstOrDefault(x => x.UserId == user.Id);
                        loginOutput.UserNo = teacher.TeacherNo;
                        loginOutput.Professional = teacher.Professional;
                        loginOutput.TeacherRank = teacher.TeacherRank;
                    }
                    loginOutput.IdCard = user.IdCard;
                    loginOutput.Phone = user.Phone;
                    loginOutput.Email = user.Email;
                    loginOutput.Gender = user.Gender;
                    loginOutput.Name = user.Name;
                    loginOutput.Token = token;
                    loginOutput.UserName = user.UserName;
                    loginOutput.UserType = user.UserType;
                    return Result.Ok(loginOutput);
                }
                else
                {
                    return Result.FromError("用户名密码错误");
                }
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Route("Logout"), HttpGet, AllowAnonymous]
        public async Task<Result> Logout()
        {

            using (var db = new ManageServerDbContext())
            {
                var user = db.Users.AsNoTracking().ToList();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 变更个人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("ChangeUser"), HttpPost]
        public async Task<Result> ChangeUser([FromBody]ChangeUserInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var userInfo = base.GetUserInfo();
                var user = db.Users.FirstOrDefault(x => x.Id == userInfo.UserId);
                user.Email = input.Email;
                user.Phone = input.Phone;
                db.SaveChanges();
            }
            return Result.Ok();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("ChangePassword"), HttpPost]
        public async Task<Result> ChangePassword([FromBody]ChangePasswordInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var userInfo = base.GetUserInfo();
                var user = db.Users.FirstOrDefault(x => x.Id == userInfo.UserId);
                user.Password = input.Password;
                db.SaveChanges();
            }
            return Result.Ok();
        }
    }
}
