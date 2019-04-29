using StudentSystem.EntityFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentSystem.Api.Models.Auth
{
    public class LoginOutput
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }
        public string Token { get; set; }
    }
}