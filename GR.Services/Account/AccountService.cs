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
        public UserModel Login(LoginViewModel model)
        {
            var user = _userRepository.Login(model.UserName, model.Password);
            if (user == null)
                return null;
            return new UserModel
            {
                UserName = user.UserName,
                UserId = user.Id
            };
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
    }
}
