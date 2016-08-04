using System;

namespace GR.Services.Account.Models
{
    /// <summary>
    /// 注册视图模型
    /// </summary>
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
