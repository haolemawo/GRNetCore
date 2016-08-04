using System;
using System.Linq;
using GR.Core.Domain.Users;

namespace GR.Data.Repository
{
    public class UserRepository
    {
        private readonly GRDbContext _dbContext;
         
        public UserRepository(GRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary> 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string userName, string password)
        {
            return _dbContext.Users.FirstOrDefault(x => x.IsActived && x.UserName == userName && x.Password == password);
        }

        /// <summary> 获取用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserBy(string userName)
        {
            return _dbContext.Users.FirstOrDefault(x => x.IsActived && x.UserName == userName);
        }


        /// <summary> 获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserBy(int userId)
        {
            return _dbContext.Users.FirstOrDefault(x => x.IsActived && x.Id == userId);
        }


        /// <summary> 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(User entity)
        {
            _dbContext.Users.Add(entity);
            return _dbContext.SaveChanges();
        }


        /// <summary> 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(User entity)
        {
            return _dbContext.SaveChanges();
        }
         
        /// <summary> 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
