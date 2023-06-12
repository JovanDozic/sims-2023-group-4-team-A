using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SIMSProject.Application.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private App App => (App)System.Windows.Application.Current;
        private readonly HttpClient _httpClient = new();
        private User _user;
        private NotificationService _notificationService;
        private ObservableCollection<Notification> _notifications = new();
        private bool _translationInProgress = false;
        private bool _leaveOnlyRecentNotifications;

        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set
            {
                if (value == _notifications) return;
                _notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }
        public Notification Notification { get; set; } = new();
        public bool TranslationInProgress
        {
            get => _translationInProgress;
            set
            {
                if (value == _translationInProgress) return;
                _translationInProgress = value;
                OnPropertyChanged(nameof(TranslationInProgress));
            }
        }

        public NotificationViewModel(User user, bool leaveOnlyRecentNotifications)
        {
            _user = user;
            _leaveOnlyRecentNotifications = leaveOnlyRecentNotifications;
            _notificationService = Injector.GetService<NotificationService>();

            CheckLanguageAndLoadNotifications();
        }

        private async void CheckLanguageAndLoadNotifications()
        {
            var notifications = _notificationService.GetAllUnreadByUser(_user);

            if (App.CurrentLanguage == "en-US")
            {
                TranslationInProgress = true;
                var titles = notifications.Select(n => n.Title).ToList();
                var descriptions = notifications.Select(n => n.Description).ToList();

                var translatedTitles = await TranslateTexts(titles);
                var translatedDescriptions = await TranslateTexts(descriptions);

                for (int i = 0; i < notifications.Count; i++)
                {
                    notifications[i].Title = translatedTitles[i];
                    notifications[i].Description = translatedDescriptions[i];
                }
            }

            if (_leaveOnlyRecentNotifications)
            {
                Notifications = new ObservableCollection<Notification>();
                if (notifications.Count > 1) Notifications.Add(notifications[0]);
            }
            else Notifications = new ObservableCollection<Notification>(notifications.OrderByDescending(x => x.CreationDate).OrderBy(x => x.IsSuggestion));
            TranslationInProgress = false;
        }

        public void MarkNotificationAsRead()
        {
            _notificationService.MarkAsRead(Notification);
            Notifications.Remove(Notification);
        }

        public async Task<List<string>> TranslateTexts(List<string> textsToTranslate)
        {
            try
            {
                var requestBody = JsonConvert.SerializeObject(textsToTranslate.Select(t => new { Text = t }));

                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri("https://api.cognitive.microsofttranslator.com" + "/translate?api-version=3.0&from=sr&to=en");
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", "efe69230a4a5406bb47416313e8678de");
                    request.Headers.Add("Ocp-Apim-Subscription-Region", "westeurope");

                    HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                    string result = await response.Content.ReadAsStringAsync();

                    return ExtractTextsFromJson(result);
                }
            }
            catch (HttpRequestException ex)
            {
                // Log exception here instead of showing message box
                return textsToTranslate;
            }
        }

        public static List<string> ExtractTextsFromJson(string jsonString)
        {
            var jsonArray = JArray.Parse(jsonString);
            return jsonArray.Select(j => j["translations"][0]["text"].Value<string>()).ToList();
        }
    }
}
