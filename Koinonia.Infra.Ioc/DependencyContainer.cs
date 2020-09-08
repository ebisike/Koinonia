using Koinonia.Application.Interface;
using Koinonia.Application.Services;
using Koinonia.Domain.Interface;
using Koinonia.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
