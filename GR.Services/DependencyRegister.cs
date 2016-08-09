using System;
using GR.Services.Account;
using GR.Services.Menus;
using Microsoft.Extensions.DependencyInjection;

namespace GR.Services
{
    public class DependencyRegister
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AccountService, AccountService>();
            services.AddScoped<MenuService, MenuService>();
        }
    }
}
