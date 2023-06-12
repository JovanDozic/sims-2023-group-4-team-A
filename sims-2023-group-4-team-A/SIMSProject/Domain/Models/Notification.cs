using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Globalization;

namespace SIMSProject.Domain.Models
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; } = new();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.MinValue;
        public DateTime? ExpirationDate { get; set; } = null;
        public bool IsRead { get; set; } = false;
        public string IconSource { get; set; } = "BellIcon";
        public bool IsSuggestion { get => Title.Contains("Predlog") || Title.Contains("Suggestion"); }

        public Notification() 
        { 
        }

        public Notification(User user, string title, string description, DateTime? expirationDate = null, string? iconSource = null)
        {
            Id = 0;
            User = user;
            Title = title;
            Description = description;
            CreationDate = DateTime.Now;
            ExpirationDate = expirationDate;
            IsRead = false;
            if (iconSource is not null) IconSource = iconSource;
        }

        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(),
                User.Id.ToString(),
                User.GetRole(User.Role),
                Title,
                Description,
                CreationDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                ExpirationDate is not null ? ((DateTime)ExpirationDate).ToString(CultureInfo.GetCultureInfo("sr-LATN")) : "",
                IconSource,
                IsRead.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            var i = 0;
            Id = int.Parse(values[i++]);
            User.Id = int.Parse(values[i++]);
            User.Role = User.GetRole(values[i++]);
            Title = values[i++];
            Description = values[i++];
            CreationDate = DateTime.Parse(values[i++], CultureInfo.GetCultureInfo("sr-LATN"));
            ExpirationDate = DateTime.TryParse(values[i++], CultureInfo.GetCultureInfo("sr-LATN"), DateTimeStyles.None, out DateTime result) ? result : null;
            IconSource = values[i++];
            IsRead = bool.Parse(values[i++]);
        }
    }
}
