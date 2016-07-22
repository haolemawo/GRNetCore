using System;
using System.Linq;
using GR.Core.Domain.Users;

namespace GR.Repository
{
    public class UserRepository
    {
        private readonly GRDbContext _dbContext;
         
        public UserRepository(GRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string userName, string password)
        {
            return _dbContext.Users.FirstOrDefault(x => x.IsActived && x.UserName == userName && x.Password == password);
        }
    }
}
