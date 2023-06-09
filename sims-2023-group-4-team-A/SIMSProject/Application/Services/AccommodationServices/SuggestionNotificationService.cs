using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class SuggestionNotificationService
    {
        private User _user;
        private NotificationService _notificationService;
        private AccommodationStatisticService _statisticService;

        public SuggestionNotificationService(User user)
        {
            _user = user;
            _notificationService = Injector.GetService<NotificationService>();
            _statisticService = Injector.GetService<AccommodationStatisticService>();
        }

        public void GenerateSuggestionNotifications()
        {
            var mostPopularLocations = _statisticService.GetMostPopularLocations();
            var leastPopularLocations = _statisticService.GetLeastPopularLocation(_user);

            NotifyAboutPopularLocations(mostPopularLocations);
            NotifyAboutLeastPopularLocations(leastPopularLocations);
        }

        private void NotifyAboutPopularLocations(List<Location> locations)
        {
            var unreadNotifications = _notificationService.GetAllUnreadByUser(_user);

            foreach (var location in locations)
            {
                var existingNotification = unreadNotifications.Find(x =>
                    x.Description.Contains(location.City) &&
                    x.Description.Contains(location.Country) &&
                    x.Description.Contains("Predlog:") &&
                    x.Description.Contains("dobra prilika"));

                if (existingNotification is null)
                {
                    var notification = new Notification(
                        _user,
                        "Predlog za otvaranje smeštaja",
                        $"Predlog: postoji dobra prilika za otvaranje novog smeštaja na lokaciji {location.City} ({location.Country})!");
                    notification.IconSource = "NotifLocationCheckIcon";
                    _notificationService.CreateNotification(notification);
                }
            }
        }

        private void NotifyAboutLeastPopularLocations(List<Location> locations)
        {
            var unreadNotifications = _notificationService.GetAllUnreadByUser(_user);

            foreach (var location in locations)
            {
                var existingNotification = unreadNotifications.Find(x =>
                    x.Description.Contains(location.City) &&
                    x.Description.Contains(location.Country) &&
                    x.Description.Contains("Predlog:") &&
                    x.Description.Contains("najnepopularnija"));

                if (existingNotification is null)
                {
                    var notification = new Notification(
                        _user,
                        "Predlog za zatvaranje smeštaja",
                        $"Predlog: lokacija {location.City} ({location.Country}) je najnepopularnija na kojoj imate smeštaj. Razmislite o zatvaranju smeštaja na toj lokaciji.");
                    notification.IconSource = "NotifLocationXIcon";
                    _notificationService.CreateNotification(notification);
                }
            }
        }

    }
}