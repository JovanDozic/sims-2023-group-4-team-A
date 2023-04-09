using Microsoft.Extensions.DependencyInjection;
using SIMSProject.Application1.Services;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.Repositories.AccommodationRepositories;
using SIMSProject.Repositories.UserRepositories;
using System;

namespace SIMSProject.Domain.Injectors
{
    public static class AppInjector
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IUserRepo, UserRepo>();
            services.AddSingleton<IAccommodationRepo, AccommodationRepo>();
            services.AddScoped<UserService>();

            return services.BuildServiceProvider();
        }
    }
}
