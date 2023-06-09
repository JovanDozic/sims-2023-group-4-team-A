using System;
using System.Globalization;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models.UserModels
{
    public class Guest1: User, ISerializable
    {
        public double Rating { get; set; }
        public int BonusPoints { get; set; }
        public DateTime? AwardDate { get; set; }

        public Guest1()
        {
        }

        public Guest1(int id, string username, string password, DateTime birthday, double rating = 0, int bonus = 0, DateTime awardDate = default(DateTime))
        {
            Id = id;
            Username = username;
            Password = password;
            Role = UserRole.Guest1;
            Rating = rating;
            Birthday = birthday;
            BonusPoints = bonus;
            AwardDate = awardDate;
        }

        public string[] ToCSV()
        {
            string awardDateValue = (AwardDate.HasValue) ? AwardDate.Value.ToString() : "";
            string[] csvValues =
            {
                Id.ToString(),
                Username,
                Password,
                GetRole(Role),
                Math.Round(Rating, 2).ToString(),
                Birthday.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                BonusPoints.ToString(),
                awardDateValue
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Password = values[2];
            Role = GetRole(values[3]);
            Rating = double.Parse(values[4]);
            Birthday = DateTime.Parse(values[5], CultureInfo.GetCultureInfo("sr-LATN"));
            BonusPoints = int.Parse(values[6]);
            if (DateTime.TryParse(values[7], CultureInfo.GetCultureInfo("sr-LATN"), DateTimeStyles.None, out DateTime awardDate))
            {
                AwardDate = awardDate;
            }
            else
            {
                AwardDate = null;
            }
        }

        public override string ToString()
        {
            return $"{Username}";
        }
    }
}
