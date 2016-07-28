using System; 
using GR.Services.Account.Models;

namespace GR.Services.Authentication
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        void SignIn(UserModel user, bool createPersistentCookie);

        /// <summary>
        /// Sign out
        /// </summary>
        void SignOut();


        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <returns>Customer</returns>
        UserModel GetAuthenticatedUser();
    }
}
