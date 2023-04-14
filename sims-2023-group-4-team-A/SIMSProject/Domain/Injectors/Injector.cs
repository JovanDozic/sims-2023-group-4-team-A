using Microsoft.Extensions.DependencyInjection;
using SIMSProject.Application.Services;
using SIMSProject.Application.Services.TourServices;
//using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.RepositoryInterfaces;
//using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
//using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.Repositories;
using SIMSProject.Repositories.TourRepositories;
//using SIMSProject.Repositories.UserRepositories;
using System;

namespace SIMSProject.Domain.Injectors
{
    public static class Injector
    {
        private static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ITourReservationRepo, TourReservationRepo>();
            services.AddSingleton<IGuideRatingRepo, GuideRatingRepo>();
            //services.AddSingleton<IUserRepo, UserRepo>();
            //services.AddSingleton<IAccommodationRepo, AccommodationRepo>();
            //services.AddSingleton<IAccommodationReservationRepo, AccommodationReservationRepo>();
            //services.AddSingleton<ILocationRepo, LocationRepo>();

            services.AddScoped<TourReservationService>();
            services.AddScoped<GuideRatingService>();
            //services.AddScoped<UserService>();
            //services.AddScoped<AccommodationService>();
            //services.AddScoped<AccommodationReservationService>();
            //services.AddScoped<LocationService>();

            return services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return Configure().GetService<T>() ?? throw new Exception("Dependency Injection Failed.");
        }
    }
}
