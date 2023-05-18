using System;
using System.Collections.Generic;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models.UserModels
{
    public class Guest : User, ISerializable
    {
        public double Rating { get; set; }
        public List<Voucher> Vouchers { get; set; } = new();
        public int BonusPoints { get; set; } 
        public bool IsAwarded { get; set; } 
        public DateTime? AwardDate { get; set; }

        public Guest()
        {
        }

        public Guest(int id, string username, string password, DateTime birthday, double rating = 0, int bonus = 0, bool isAwarded = false, DateTime awardDate = default(DateTime))
        {
            Id = id;
            Username = username;
            Password = password;
            Role = UserRole.Guest;
            Rating = rating;
            Birthday = birthday;
            BonusPoints = bonus;
            IsAwarded = isAwarded;
            AwardDate = awardDate;
            Vouchers = new List<Voucher>();
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
                Birthday.ToString(),
                BonusPoints.ToString(),
                IsAwarded.ToString(),
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
            Birthday = DateTime.Parse(values[5]);
            BonusPoints = int.Parse(values[6]);
            IsAwarded = bool.Parse(values[7]);
            if (DateTime.TryParse(values[8], out DateTime awardDate))
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