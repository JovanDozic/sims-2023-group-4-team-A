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

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private App App => (App)System.Windows.Application.Current;
        private User _user;
        private NotificationService _notificationService;

        public ObservableCollection<Notification> Notifications { get; set; } = new();
        public Notification Notification { get; set; } = new();

        public NotificationViewModel(User user)
        {
            _user = user;

            _notificationService = Injector.GetService<NotificationService>();

            CheckLanguageAndLoadNotifications();
        }

        private async void CheckLanguageAndLoadNotifications()
        {
            var notifications = _notificationService.GetAllUnreadByUser(_user);
            if (App.CurrentLanguage == "en-US")
            {
                foreach (Notification notification in notifications)
                {
                    notification.Title = await TranslateText(notification.Title);
                    notification.Description = await TranslateText(notification.Description);
                }
            }
            Notifications = new(notifications);
        }

        public void MarkNotificationAsRead()
        {
            _notificationService.MarkAsRead(Notification);
            Notifications.Remove(Notification);
        }

        public async Task<string> TranslateText(string textToTranslate)
        {
            try
            {
                object[] body = new object[] { new { Text = textToTranslate } };
                var requestBody = JsonConvert.SerializeObject(body);

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri("https://api.cognitive.microsofttranslator.com" + "/translate?api-version=3.0&from=sr&to=en");
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", "efe69230a4a5406bb47416313e8678de");
                    request.Headers.Add("Ocp-Apim-Subscription-Region", "westeurope");

                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    string result = await response.Content.ReadAsStringAsync();

                    string translatedText = ExtractTextFromJson(result);
                    MessageBox.Show(translatedText);
                    return translatedText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Translation failed: " + ex.Message);
                return string.Empty;
            }
        }

        public static string ExtractTextFromJson(string jsonString)
        {
            var jsonArray = JArray.Parse(jsonString);
            var text = jsonArray[0]["translations"][0]["text"].Value<string>();
            return text;
        }
    }
}
