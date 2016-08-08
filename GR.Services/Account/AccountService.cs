using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using GR.Core;
using GR.Data.Repository;
using GR.Services.Account.Models;

namespace GR.Services.Account
{
    public class AccountService
    {
        private readonly UserRepository _userRepository;

        public AccountService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<UserModel> Login(LoginViewModel model)
        {
            var result = new ReturnResult<UserModel>();
            var user = _userRepository.Login(model.UserName, model.Password);
            //
            if (user == null)
            {
                result.IsSuccess = false;
                result.Message = "用户不存在，请重新登陆";
                return result;
            }
            result.IsSuccess = true;
            result.Message = "";
            result.Data = new UserModel
            {
                UserName = user.UserName,
                UserId = user.Id
            };
            return result;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ClaimsPrincipal SignIn(LoginViewModel model)
        {
            var user = _userRepository.Login(model.UserName, model.Password);
            if (user == null)
                return null;
            //
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, Constants.CONSTANTS_ISSUER));
            claims.Add(new Claim(ClaimTypes.UserData, user.Id.ToString(), ClaimValueTypes.String, Constants.CONSTANTS_ISSUER));
            if (user.UserRoles != null && user.UserRoles.Count > 0)
            {
                StringBuilder userRoles = new StringBuilder();
                user.UserRoles.ForEach(x =>
                {
                    userRoles.AppendFormat("{0},", x.Role.RoleName);
                });
                //这里写死了，正式的话，角色需要通过获取数据库角色，写入的
                //claims.Add(new Claim(ClaimTypes.Role, "Administrator", ClaimValueTypes.String, Constants.CONSTANTS_ISSUER));
                claims.Add(new Claim(ClaimTypes.Role, userRoles.ToString().TrimEnd(','), ClaimValueTypes.String, Constants.CONSTANTS_ISSUER));
            }
            //
            var userIdentity = new ClaimsIdentity(Constants.CONSTANTS_CLAIMS_IDENTITY_AUTHENTICATION_TYPE);
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            return userPrincipal;
        }

        /// <summary> 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult ChangePassword(ChangePasswordViewModel model, string userName)
        {
            var result = new ReturnResult
            {
                IsSuccess = true,
                Message = "密码修改成功，请重新登录"
            };
            var user = _userRepository.GetUserBy(userName);
            if (user == null)
            {
                result.IsSuccess = false;
                result.Message = "用户不存在";
                return result;
            }
            if (user.Password != model.OldPassword)
            {
                result.IsSuccess = false;
                result.Message = "原始密码输入错误";
                return result;
            }
            user.Password = model.NewPassword;
            if (_userRepository.Update(user) <= 0)
            {
                result.IsSuccess = false;
                result.Message = "密码修改失败，请重试";
            }
            return result;
        }

        public ReturnResult<UserProfileViewModel> GetUserProfile(string userName)
        {
            var result = new ReturnResult<UserProfileViewModel>();

            if (string.IsNullOrEmpty(userName))
            {
                result.IsSuccess = false;
                result.Message = "用户不存在，请重新登陆";
                return result;
            }
            var user = _userRepository.GetUserBy(userName);
            if (user == null)
            {
                result.IsSuccess = false;
                result.Message = "用户不存在，请重新登陆";
                return result;
            }
            result.IsSuccess = true;
            result.Message = "";
            result.Data = new UserProfileViewModel
            {
                UserName = user.UserName
            };
            return result;
        }
    }
}
