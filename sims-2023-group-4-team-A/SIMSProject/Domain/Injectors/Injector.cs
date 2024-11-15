﻿using Microsoft.Extensions.DependencyInjection;
using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces.GuideRepositoryInterfaces;
using SIMSProject.Repositories;
using SIMSProject.Repositories.AccommodationRepositories;
using SIMSProject.Repositories.TourRepositories;
using SIMSProject.Repositories.UserRepositories;
using SIMSProject.Repositories.UserRepositories.GuideRepositories;
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
            services.AddSingleton<IGuest2Repo, Guest2Repo>();
            services.AddSingleton<IGuest1Repo, Guest1Repo>();
            services.AddSingleton<IGuideRepo, GuideRepo>();
            services.AddSingleton<ILocationRepo, LocationRepo>();
            services.AddScoped<INotificationRepo, NotificationRepo>();
            services.AddSingleton<IUserRepo, UserRepo>(
                provider => new UserRepo(
                    provider.GetService<IOwnerRepo>() ?? throw new Exception("Dependency Injection Failed: IOwnerRepo not found."),
                    provider.GetService<IOwnerRatingRepo>() ?? throw new Exception("Dependency Injection Failed: IOwnerRatingRepo not found."),
                    provider.GetService<IGuideRepo>() ?? throw new Exception("Dependency Injection Failed: IGuideRepo not found."),
                    provider.GetService<IGuideRatingRepo>() ?? throw new Exception("Dependency Injection Failed: IGuideRatingRepo not found."),
                    provider.GetService<IGuest2Repo>() ?? throw new Exception("Dependency Injection Failed: IGuestRepo not found."),                  
                    provider.GetService<IGuestRatingRepo>() ?? throw new Exception("Dependency Injection Failed: IGuestRatingRepo not found."),
                    provider.GetService<IGuest1Repo>() ?? throw new Exception("Dependency Injection Failed: IGuest1Repo not found.")
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
                    provider.GetService<IGuest1Repo>() ?? throw new Exception("Dependency Injection Failed: IGuest1Repo not found.")
                )
            );
            services.AddSingleton<IAccommodationRenovationRepo, AccommodationRenovationRepo>(
                provider => new AccommodationRenovationRepo(
                    provider.GetService<IAccommodationRepo>() ?? throw new Exception("Dependency Injection Failed: IAccommodationRepo not found.")
                )
            );
            services.AddSingleton<IGuestRatingRepo, GuestRatingRepo>(
                provider => new GuestRatingRepo(
                    provider.GetService<IAccommodationReservationRepo>() ?? throw new Exception("Dependency Injection Failed: IAccommodationReservationRepo not found.")
                )
            );
            services.AddSingleton<IRenovationSuggestionRepo, RenovationSuggestionRepo>();
            services.AddSingleton<IForumRepo, ForumRepo>();

            services.AddSingleton<IOwnerRatingRepo, OwnerRatingRepo>(
                provider => new OwnerRatingRepo(
                    provider.GetService<IAccommodationReservationRepo>() ?? throw new Exception("Dependency Injection Failed: IAccommodationReservationRepo not found."),
                    provider.GetService<IRenovationSuggestionRepo>() ?? throw new Exception("Dependency Injection Failed: IRenovationSuggestionRepo not found.")
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
                    provider.GetService<IGuest2Repo>() ?? throw new Exception("Dependency Injection Failed: IGuestRepo not found."),
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
            services.AddSingleton<ICustomTourRequestRepo, CustomTourRequestRepo>(
                provider => new CustomTourRequestRepo(
                    provider.GetService<IGuest2Repo>() ?? throw new Exception("Dependency Injection Failed: IGuestRepo not found."),
                    provider.GetService<ILocationRepo>() ?? throw new Exception("Dependency Injection Failed: ILocationRepo not found.")
                )
            );
            services.AddSingleton<ITourRatingRepo, TourRatingRepo>(
                provider => new TourRatingRepo(
                    provider.GetService<IGuideRatingRepo>() ?? throw new Exception("Dependency Injection Failed: IGuideRatingRepo not found."),
                    provider.GetService<ITourGuestRepo>() ?? throw new Exception("Dependency Injection Failed: ITourGuestRepo not found.")
                    )
                );

            services.AddSingleton<ITourStatisticsRepo, TourStatisticsRepo>(
                provider => new TourStatisticsRepo(
                    provider.GetService<ITourReservationRepo>() ?? throw new Exception("Dependency Injection Failed: ITourReservationRepo not found."),
                    provider.GetService<ITourGuestRepo>() ?? throw new Exception("Dependency Injection Failed: ITourGuestRepo not found.")
                    )
                );

            services.AddSingleton<ISuperGuideLogRepo, SuperGuideLogRepo>();
            services.AddSingleton<IComplexTourRequestRepo, ComplexTourRequestRepo>(
                provider => new ComplexTourRequestRepo(
                    provider.GetService<IGuest2Repo>() ?? throw new Exception("Dependency Injection Failed: IGuestRepo not found."),
                    provider.GetService<ICustomTourRequestRepo>() ?? throw new Exception("Dependency Injection Failed: ICustomTourRequestRepo not found.")
                )
            );

            services.AddSingleton<ICommentRepo, CommentRepo>(
                provider => new CommentRepo(
                    provider.GetService<IUserRepo>() ?? throw new Exception("Dependency Injection Failed: IUserRepo not found.")
                    )
                );

            services.AddSingleton<IForumRepo, ForumRepo>(
                provider => new ForumRepo(
                    provider.GetService<ILocationRepo>() ?? throw new Exception("Dependency Injection Failed: ILocationRepo not found."), 
                    provider.GetService<ICommentRepo>() ?? throw new Exception("Dependency Injection Failed: ICommentRepo not found.")
                    )
                );

            // Service Injections
            services.AddScoped<UserService>();
            services.AddScoped<AccommodationService>();
            services.AddScoped<AccommodationReservationService>();
            services.AddScoped<AccommodationRenovationService>();
            services.AddScoped<AccommodationStatisticService>();
            services.AddScoped<LocationService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<GuestRatingService>();
            services.AddScoped<OwnerRatingService>();
            services.AddScoped<ReschedulingRequestService>();
            services.AddScoped<TourService>();
            services.AddScoped<TourAppointmentService>();
            services.AddScoped<TourGuestService>();
            services.AddScoped<TourKeyPointService>();
            services.AddScoped<VoucherService>();
            services.AddScoped<KeyPointService>();
            services.AddScoped<TourReservationService>();
            services.AddScoped<GuideRatingService>();
            services.AddScoped<TourStatisticsService>();
            services.AddScoped<CustomTourRequestService>();
            services.AddScoped<TourRatingService>();
            services.AddScoped<CustomTourRequestStatisticsService>();
            services.AddScoped<RenovationSuggestionService>();
            services.AddScoped<ForumService>();
            services.AddScoped<CommentService>();
            services.AddScoped<GuideService>();
            services.AddScoped<ComplexTourRequestService>();

            return services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return Configure().GetService<T>() ?? throw new Exception("Dependency Injection Failed: " + nameof(T) + " not found.");
        }
    }
}
