using System; 
using GR.Repository;
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
    }
}
