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
        [Route("Login"), HttpGet, AllowAnonymous]
        public async Task<Result> Login([FromUri]LoginInput input)
        {
            using (var db = new ManageServerDbContext())
            {
                var user = db.Users.AsNoTracking().SingleOrDefault(x => x.Password == input.Password && x.UserName == input.UserName && x.UserType == input.UserType);
                if (user != null)
                {
                    var cache = new Cache();
                    var token = new Guid().ToString();
                    var userInfo = new UserInfo();
                    userInfo.UserType = user.UserType;
                    userInfo.Name = user.Name;
                    userInfo.UserId = user.Id;
                    userInfo.UserName = user.UserName;
                    cache.Insert(token, userInfo);
                    return Result.Ok(token);
                }
                else
                {
                    return Result.FromError("用户名密码错误");
                }
            }
        }

        [Route("Logout"), HttpGet]
        public async Task<Result> Logout()
        {
            return Result.Ok();
        }

    }
}
