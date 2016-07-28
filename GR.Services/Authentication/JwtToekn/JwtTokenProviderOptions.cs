using System;
using Microsoft.IdentityModel.Tokens;

namespace GR.Services.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 原文地址：http://www.cnblogs.com/indexlang/p/indexlang.html
    /// </remarks>
    public class JwtTokenProviderOptions
    {
        public string Path { get; set; } = "/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);

       public SigningCredentials SigningCredentials { get; set; }
    }
}
