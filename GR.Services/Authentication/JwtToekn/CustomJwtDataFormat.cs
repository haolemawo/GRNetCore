﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace GR.Services.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 原文地址：http://www.cnblogs.com/indexlang/p/indexlang.html
    /// </remarks>
    public class CustomJwtDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _algorithm;
        private readonly TokenValidationParameters _validationParameters;

        public CustomJwtDataFormat(string algorithm, TokenValidationParameters validationParameters)
        {
            _algorithm = algorithm;
            _validationParameters = validationParameters;
        }

        // This ISecureDataFormat implementation is decode-only
        public string Protect(AuthenticationTicket data)
        {
            throw new NotImplementedException();
        }

        public string Protect(AuthenticationTicket data, string purpose)
        {
            throw new NotImplementedException();
        }

        //public AuthenticationTicket Unprotect(string protectedText)
        //{
        //    return Unprotect(protectedText, null);
        //}
        public AuthenticationTicket Unprotect(string protectedText) => Unprotect(protectedText, null);

        public AuthenticationTicket Unprotect(string protectedText, string purpose)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal;
            SecurityToken validToken;
            //
            try {
                principal = handler.ValidateToken(protectedText, _validationParameters, out validToken);
                var validJwt = validToken as JwtSecurityToken;
                if (validJwt == null)
                    throw new ArgumentNullException("Invalid JWT");
                if (!validJwt.Header.Alg.Equals(_algorithm, StringComparison.Ordinal))
                    throw new ArgumentException($"Algorithm must be '{_algorithm}'");
                // Additional custom validation of JWT claims here (if any)
            }
            catch (SecurityTokenValidationException ex)
            {
                return null;
            }
            // Validation passed. Return a valid AuthenticationTicket:
            return new AuthenticationTicket(principal, new AuthenticationProperties(), "Cookie");
        }
    }
}
