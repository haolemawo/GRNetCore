using System;

namespace GR.Services.Account.Models
{
    /// <summary>
    /// 修改密码视图模型
    /// </summary>
    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
        
        public string ConfirmPassword { get; set; }
    }
}
