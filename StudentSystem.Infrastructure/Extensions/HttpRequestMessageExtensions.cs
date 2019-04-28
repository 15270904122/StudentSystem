using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Infrastructure.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// 设置 Cookie 验证授权
        /// </summary>
        public static void SetCookieTicket(this HttpRequestMessage request, string ticket)
        {
            if (ticket == null)
                request.Properties.Remove("CookieAuth");
            else
                request.Properties["CookieAuth"] = ticket;
        }


        /// <summary>
        /// 获取 Cookie 验证授权
        /// </summary>
        public static object GetCookieTicket(this HttpRequestMessage request)
        {
            return request.Properties.TryGetValue("CookieAuth", out object obj)
                ? obj
                : null;
        }
    }
}
