using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Infrastructure.Result
{
    /// <summary>
    ///结果状态码
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 操作成功
        ///</summary>
        [Display(Name = "操作成功", GroupName = Result.Successfull)]
        Ok = 0,

        /// <summary>
        /// 操作失败
        ///</summary>
        [Display(Name = "操作失败")]
        Fail = 1,

        /// <summary>
        /// 服务数据异常
        ///</summary>
        [Display(Name = "服务数据异常")]
        ServerError = 10,

        /// <summary>
        /// 未授权
        ///</summary>
        [Display(Name = "未授权")]
        Unauthorized = 401,

        /// <summary>
        /// 未登录
        /// </summary>
        [Display(Name = "未登录")]
        Forbidden = 21,

        /// <summary>
        /// Token 失效
        /// </summary>
        [Display(Name = "Token 失效")]
        InvalidToken = 22,

        /// <summary>
        /// 密码验证失败
        /// </summary>
        [Display(Name = "密码验证失败")]
        SpaFailed = 23,

        /// <summary>
        /// 错误的新密码
        /// </summary>
        [Display(Name = "错误的新密码")]
        WrongNewPassword = 24,
    }
}
