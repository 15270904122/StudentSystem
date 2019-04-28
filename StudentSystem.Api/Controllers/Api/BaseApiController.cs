using StudentSystem.Api.Claims;
using StudentSystem.Api.Models.Auth;
using StudentSystem.EntityFramework.Core;
using System.Security.Claims;
using System.Web.Http;

namespace StudentSystem.Api.Controllers.Api
{
    [Authorize]
    public class BaseApiController : ApiController
    {

        internal string GetUserId()
        {
            var identity = this.User.Identity as ClaimsIdentity;
            foreach (var claim in identity.Claims)
            {
                if (claim.Type == ManageServerClaimType.UserId)
                {
                    return claim.Value;
                }
            }
            return "";
        }

        internal UserInfo GetUserInfo()
        {
            var identity = this.User.Identity as ClaimsIdentity;
            var userInfo = new UserInfo();
            foreach (var claim in identity.Claims)
            {
                if (claim.Type == ManageServerClaimType.UserId)
                {
                    userInfo.UserId = long.Parse(claim.Value);
                }
                if (claim.Type == ManageServerClaimType.UserType)
                {
                    userInfo.UserType = (UserType)int.Parse(claim.Value);
                }
                if (claim.Type == ManageServerClaimType.UserName)
                {
                    userInfo.UserName = claim.Value;
                }

            }
            return userInfo;
        }
    }
}
