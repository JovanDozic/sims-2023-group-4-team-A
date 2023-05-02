using Microsoft.Extensions.DependencyInjection;
using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
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
            services.AddSingleton<IOwnerRepo, OwnerRepo>();
            services.AddSingleton<IGuestRepo, GuestRepo>();
            services.AddSingleton<IGuideRepo, GuideRepo>();
            services.AddSingleton<ILocationRepo, LocationRepo>();
            services.AddScoped<INotificationRepo, NotificationRepo>();
            services.AddSingleton<IUserRepo, UserRepo>(
                provider => new UserRepo(
                    provider.GetService<IOwnerRepo>() ?? throw new Exception("Dependency Injection Failed: IOwnerRepo not found."),
                    provider.GetService<IOwnerRatingRepo>() ?? throw new Exception("Dependency Injection Failed: IOwnerRatingRepo not found."),
                    provider.GetService<IGuideRepo>() ?? throw new Exception("Dependency Injection Failed: IGuideRepo not found."),
                    provider.GetService<IGuideRatingRepo>() ?? throw new Exception("Dependency Injection Failed: IGuideRatingRepo not found."),
                    provider.GetService<IGuestRepo>() ?? throw new Exception("Dependency Injection Failed: IGuestRepo not found."),
                    provider.GetService<IGuestRatingRepo>() ?? throw new Exception("Dependency Injection Failed: IGuestRatingRepo not found.")
                )
            );
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
            services.AddSingleton<IOwnerRatingRepo, OwnerRatingRepo>(
                provider => new OwnerRatingRepo(
                    provider.GetService<IAccommodationReservationRepo>() ?? throw new Exception("Dependency Injection Failed: IAccommodationReservationRepo not found.")
                )
            );
            services.AddSingleton<IReschedulingRequestRepo, ReschedulingRequestRepo>(
                provider => new ReschedulingRequestRepo(
                    provider.GetService<IAccommodationReservationRepo>() ?? throw new Exception("Dependency Injection Failed: IAccommodationReservationRepo not found.")
                )
            );
            services.AddSingleton<IKeyPointRepo, KeyPointRepo>(
               provider => new KeyPointRepo(
                   provider.GetService<ILocationRepo>() ?? throw new Exception("Dependency Injection Failed: ILocationRepo not found.")
                   )
               );
            services.AddSingleton<ITourKeyPointRepo, TourKeyPointRepo>();
            services.AddSingleton<ITourGuestRepo, TourGuestRepo>(
                provider => new TourGuestRepo(
                    provider.GetService<IKeyPointRepo>() ?? throw new Exception("Dependency Injection Failed: IKEyPointRepo not found."),
                    provider.GetService<IGuestRepo>() ?? throw new Exception("Dependency Injection Failed: IGuestRepo not found."),
                    provider.GetService<ITourAppointmentRepo>() ?? throw new Exception("Dependency Injection Failed: ITourAppointmentRepo not found.")
                    )
                );
            services.AddSingleton<IVoucherRepo, VoucherRepo>();
            services.AddSingleton<ITourRepo, TourRepo>(
                provider => new TourRepo(
                    provider.GetService<ITourKeyPointRepo>() ?? throw new Exception("Dependency Injection Failed: ITourKeyPointRepo not found."),
                    provider.GetService<IKeyPointRepo>() ?? throw new Exception("Dependency Injection Failed: IKeyPointRepo not found."),
                    provider.GetService<ILocationRepo>() ?? throw new Exception("Dependency Injection Failed: ILocationRepo not found.")
                    )
                );
            services.AddSingleton<ITourAppointmentRepo, TourAppointmentRepo>(
                provider => new TourAppointmentRepo(
                    provider.GetService<IKeyPointRepo>() ?? throw new Exception("Dependency Injection Failed: IKeyPointRepo not found."),
                    provider.GetService<ITourRepo>() ?? throw new Exception("Dependency Injection Failed: ITourRepo not found."),
                    provider.GetService<IGuideRepo>() ?? throw new Exception("Dependency Injection Failed: IGuideRepo not found.") 
                    ));
            services.AddSingleton<ITourReservationRepo, TourReservationRepo>(
                provider => new TourReservationRepo(
                    provider.GetService<ITourAppointmentRepo>() ?? throw new Exception("Dependency Injection Failed: ITourAppointmentRepo not found.")
                )
            );
            services.AddSingleton<IGuideRatingRepo, GuideRatingRepo>(
                provider => new GuideRatingRepo(
                    provider.GetService<ITourReservationRepo>() ?? throw new Exception("Dependency Injection Failed: ITourReservationRepo not found.")
                )
            );

            // Service Injections
            services.AddScoped<UserService>();
            services.AddScoped<AccommodationService>();
            services.AddScoped<AccommodationReservationService>();
            services.AddScoped<LocationService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<GuestRatingService>();
            services.AddScoped<OwnerRatingService>();
            services.AddScoped<ReschedulingRequestService>();
            services.AddScoped<TourService>();
            services.AddScoped<TourAppointmentService>();
            services.AddScoped<TourGuestService>();
            services.AddScoped<TourKeyPointService>();
            services.AddScoped<VoucherSevice>();
            services.AddScoped<KeyPointService>();
            services.AddScoped<TourReservationService>();
            services.AddScoped<GuideRatingService>();

            return services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return Configure().GetService<T>() ?? throw new Exception("Dependency Injection Failed: " + nameof(T) + " not found.");
        }
    }
}
