using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.Auth
{
    public class LoginInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
