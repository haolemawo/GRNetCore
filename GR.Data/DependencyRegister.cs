﻿using System;
using GR.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GR.Data
{
    public class DependencyRegister
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GRDbContext>(ServiceLifetime.Singleton);
            services.AddScoped<UserRepository, UserRepository>();
        }
    }
}
