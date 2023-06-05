using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; } = new();
        public string Text { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int Downvotes { get; set; } = 0;

        public Comment()
        {
        }

        public Comment(Comment original)
        {
            if (original is null) return;
            Id = original.Id;
            User = original.User;
            Text = original.Text;
            CreationDate = original.CreationDate;
            Downvotes = original.Downvotes;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                User.Id.ToString(),
                Text,
                CreationDate.ToString(),
                Downvotes.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            User.Id = int.Parse(values[i++]);
            Text = values[i++];
            CreationDate = DateTime.Parse(values[i++]);
            Downvotes = int.Parse(values[i++]);
        }
    }
}