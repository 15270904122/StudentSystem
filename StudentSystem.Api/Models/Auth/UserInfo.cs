using StudentSystem.EntityFramework.Core;

namespace StudentSystem.Api.Models.Auth
{
    public class UserInfo
    {
        public UserType UserType { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}
