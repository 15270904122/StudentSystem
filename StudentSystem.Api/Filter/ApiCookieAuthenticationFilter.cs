using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using StudentSystem.Api.Claims;
using StudentSystem.Api.Models.Auth;
using StudentSystem.Infrastructure.Extensions;
using StudentSystem.Infrastructure.Result;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace StudentSystem.Api.Filter
{

    public class ApiCookieAuthenticationFilter : IAuthenticationFilter, System.Web.Http.Filters.IActionFilter
    {
        private readonly string _cookieName;


        public bool AllowMultiple { get { return false; } }


        public ApiCookieAuthenticationFilter(string cookieName)
        {
            this._cookieName = cookieName;
        }

        /// <summary>
        /// 用户认证
        /// </summary>
        public async Task AuthenticateAsync(HttpAuthenticationContext authenticationContext, CancellationToken cancellationToken)
        {
            if (!authenticationContext.Principal.Identity.IsAuthenticated)
            {
                var cookieValue = authenticationContext.Request.Headers.GetCookies(this._cookieName).FirstOrDefault();
                if (cookieValue != null)
                {
                    try
                    {
                        var userInfo = JsonConvert.DeserializeObject<UserInfo>(cookieValue[this._cookieName].Value);
                        var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                        identity.AddClaim(new Claim(ManageServerClaimType.UserType, ((int)userInfo.UserType).ToString()));
                        identity.AddClaim(new Claim(ManageServerClaimType.UserId, userInfo.UserId.ToString()));
                        identity.AddClaim(new Claim(ManageServerClaimType.UserName, userInfo.UserName));
                        authenticationContext.Principal = new ClaimsPrincipal(identity);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {

                }

            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var response = await continuation();
            var ticket = actionContext.Request.GetCookieTicket() as string;
            if (ticket != null)
            {
                var cookie = new CookieHeaderValue(this._cookieName, ticket)
                {
                    HttpOnly = true,
                    Path = "/"
                };

                response.Headers.AddCookies(new[] { cookie });
            }
            else
            {
                var result = Result.FromCode(ResultCode.Unauthorized);
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
                httpResponseMessage.Content = new ObjectContent<Result>(result, formatter, "application/json");
                actionContext.Response = httpResponseMessage;
            }
            return response;
        }

    }
}
