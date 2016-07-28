using System;
using GR.Services.Account.Models;
using Microsoft.AspNetCore.Authentication;

namespace GR.Services.Authentication
{
    public class FormsAuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <returns>Customer</returns>
        public UserModel GetAuthenticatedUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        public void SignIn(UserModel user, bool createPersistentCookie)
        {
            //throw new NotImplementedException();
           // var 
          //var ticket=new AuthenticationTicket()
        }

        /// <summary>
        /// Sign out
        /// </summary>
        public void SignOut()
        {
            throw new NotImplementedException();
        }
    }
}
