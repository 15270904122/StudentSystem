using Microsoft.AspNet.Identity;
using StudentSystem.Api.Claims;
using StudentSystem.Api.Models.Auth;
using StudentSystem.Infrastructure.Result;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace StudentSystem.Api.Filter
{

    public class ApiTokenAuthenticationFilter : IAuthenticationFilter, IAuthorizationFilter
    {


        public bool AllowMultiple { get { return false; } }


        public ApiTokenAuthenticationFilter()
        {
        }

        /// <summary>
        /// 用户认证
        /// </summary>
        public async Task AuthenticateAsync(HttpAuthenticationContext authenticationContext, CancellationToken cancellationToken)
        {
            if (authenticationContext.Request.GetActionDescriptor().GetCustomAttributes<AllowAnonymousAttribute>().Any()
               || authenticationContext.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                return;
            else if (!authenticationContext.Request.Headers.TryGetValues("Token", out var tokens))
                authenticationContext.ActionContext.Response = Result.FromCode(ResultCode.Unauthorized, "未登录").ToHttpResponseMessage();
            else
            {
                string token = tokens.First();
                var cache = new Cache();
                var userInfo = (UserInfo)cache.Get(token);
                if (userInfo == null)
                {
                    authenticationContext.ActionContext.Response = Result.FromCode(ResultCode.Unauthorized, "未登录").ToHttpResponseMessage();
                }
                else
                {
                    var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ExternalBearer);
                    identity.AddClaim(new Claim(ManageServerClaimType.UserType, ((int)userInfo.UserType).ToString()));
                    identity.AddClaim(new Claim(ManageServerClaimType.UserId, userInfo.UserId.ToString()));
                    identity.AddClaim(new Claim(ManageServerClaimType.UserName, userInfo.UserName));
                    authenticationContext.Principal = new ClaimsPrincipal(identity);
                }

            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.Response != null)
            {
                return actionContext.Response;
            }
            return await continuation();
        }
    }

    public static class ResultExtension
    {
        public static HttpResponseMessage ToHttpResponseMessage(this Result result)
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            httpResponseMessage.Content = new ObjectContent<Result>(result, formatter, "application/json");
            return httpResponseMessage;
        }
    }
}
