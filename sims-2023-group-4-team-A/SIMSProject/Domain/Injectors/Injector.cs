using Microsoft.Extensions.DependencyInjection;
using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.Repositories;
using SIMSProject.Repositories.AccommodationRepositories;
using SIMSProject.Repositories.UserRepositories;
using System;

namespace SIMSProject.Domain.Injectors
{
    public static class Injector
    {
        private static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            // Basic Repo Injections
            services.AddSingleton<IUserRepo, UserRepo>();
            services.AddSingleton<IOwnerRepo, OwnerRepo>();
            services.AddSingleton<IGuestRepo, GuestRepo>();
            services.AddSingleton<ILocationRepo, LocationRepo>();

            // Complex Repo Injections
            services.AddSingleton<IAccommodationRepo, AccommodationRepo>(
                provider => new AccommodationRepo(
                    provider.GetService<ILocationRepo>() ?? throw new Exception("Dependency Injection Failed: ILocationRepo not found."),
                    provider.GetService<IOwnerRepo>() ?? throw new Exception("Dependency Injection Failed: IOwnerRepo not found.")
                )
            );
            services.AddSingleton<IAccommodationReservationRepo, AccommodationReservationRepo>(
                provider => new AccommodationReservationRepo(
                    provider.GetService<IAccommodationRepo>() ?? throw new Exception("Dependency Injection Failed: IAccommodationRepo not found."),
                    provider.GetService<IGuestRepo>() ?? throw new Exception("Dependency Injection Failed: IGuestRepo not found.")
                )
            );
            services.AddSingleton<IGuestRatingRepo, GuestRatingRepo>(
                provider => new GuestRatingRepo(
                    provider.GetService<IAccommodationReservationRepo>() ?? throw new Exception("Dependency Injection Failed: IAccommodationReservationRepo not found.")
                )
            );

            // Service Injections
            services.AddScoped<UserService>();
            services.AddScoped<AccommodationService>();
            services.AddScoped<AccommodationReservationService>();
            services.AddScoped<LocationService>();
            services.AddScoped<GuestRatingService>();
            //services.AddScoped<GuestRatingService>(
            //    provider => new GuestRatingService(
            //        provider.GetService<IGuestRatingRepo>() ?? throw new Exception("Dependency Injection Failed: IGuestRatingRepo not found."),
            //        provider.GetService<IGuestRepo>() ?? throw new Exception("Dependency Injection Failed: IGuestRepo not found.")
            //    )
            //);


            return services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return Configure().GetService<T>() ?? throw new Exception("Dependency Injection Failed: " + nameof(T) + " not found.");
        }
    }
}
