using Newtonsoft.Json;
using StudentSystem.Api.Claims;
using StudentSystem.Api.Models.Auth;
using System;
using System.Security.Claims;
using System.Web.Mvc.Filters;

namespace StudentSystem.Api.Filter
{

    /// <inheritdoc/>
    public class MvcCookieAuthenticationFilter : System.Web.Mvc.Filters.IAuthenticationFilter
    {
        private readonly string _cookieName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcCookieAuthenticationFilter" /> class.
        /// </summary>
        /// <param name="dataProtectionProvider">The data protection provider.</param>
        /// <param name="cookieName">Name of the cookie.</param>
        public MvcCookieAuthenticationFilter(string cookieName)
        {
            this._cookieName = cookieName;
        }

        /// <summary>
        /// 对请求进行身份验证。
        /// </summary>
        /// <param name="filterContext">用于身份验证的上下文。</param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var cookieValue = filterContext.HttpContext.Request.Cookies.Get(this._cookieName);
                if (cookieValue != null)
                {
                    try
                    {
                        var userInfo = JsonConvert.DeserializeObject<UserInfo>(cookieValue.Value);
                        var identity = new ClaimsIdentity();
                        identity.AddClaim(new Claim(ManageServerClaimType.UserType, ((int)userInfo.UserType).ToString()));
                        identity.AddClaim(new Claim(ManageServerClaimType.UserId, userInfo.UserId.ToString()));
                        identity.AddClaim(new Claim(ManageServerClaimType.UserName, userInfo.UserName));
                        filterContext.Principal = new ClaimsPrincipal(identity);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}
