using Microsoft.Extensions.DependencyInjection;
using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.Repositories;
using SIMSProject.Repositories.AccommodationRepositories;
using SIMSProject.Repositories.TourRepositories;
using SIMSProject.Repositories.UserRepositories;
using System;

namespace SIMSProject.Domain.Injectors
{
    public static class Injector
    {
        private static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            // Repository Injections
            services.AddSingleton<IUserRepo, UserRepo>();
            services.AddSingleton<IOwnerRepo, OwnerRepo>();
            services.AddSingleton<IGuestRepo, GuestRepo>();
            services.AddSingleton<ILocationRepo, LocationRepo>();
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
            services.AddSingleton<IReschedulingRequestRepo, ReschedulingRequestRepo>(
                provider => new ReschedulingRequestRepo(
                    provider.GetService<IAccommodationReservationRepo>() ?? throw new Exception("Dependency Injection Failed: IAccommodationReservationRepo not found.")
                )
            );

            services.AddSingleton<ITourRepo, TourRepo>();
            services.AddSingleton<ITourAppointmentRepo, TourAppointmentRepo>();
            services.AddSingleton<ITourKeyPointRepo, TourKeyPointRepo>();
            services.AddSingleton<ITourGuestRepo, TourGuestRepo>();
            services.AddSingleton<IKeyPointRepo, KeyPointRepo>();
            services.AddSingleton<IVoucherRepo, VoucherRepo>();



            // Service Injections
            services.AddScoped<UserService>();
            services.AddScoped<AccommodationService>();
            services.AddScoped<AccommodationReservationService>();
            services.AddScoped<LocationService>();
            services.AddScoped<GuestRatingService>();
            services.AddScoped<ReschedulingRequestService>();

            services.AddScoped<TourService>();
            services.AddTransient<TourAppointmentService>();
            services.AddScoped<TourGuestService>();
            services.AddScoped<TourKeyPointService>();
            services.AddScoped<VoucherSevice>();
            services.AddScoped<KeyPointService>();
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
            // Try if service no work!
            //services.AddScoped<ServiceName>(
            //    provider => new ServiceName(
            //        provider.GetService<RepoThatThatServiceUses1>() ?? throw new Exception("Dependency Injection Failed: RepoThatThatServiceUses1 not found."),
            //        provider.GetService<RepoThatThatServiceUses2>() ?? throw new Exception("Dependency Injection Failed: RepoThatThatServiceUses2 not found.")
            //        // ...
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
