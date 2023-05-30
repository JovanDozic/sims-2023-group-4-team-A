using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class Forum: ISerializable
    {
        public int Id { get; set; }
        public Guest1 ForumOwner { get; set; } = new();
        public Location Location { get; set; } = new();
        public string Comment { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }

        public Forum()
        {
        }

        public Forum(Forum forum)
        {
            Id = forum.Id;
            ForumOwner.Id = forum.ForumOwner.Id;
            Location = forum.Location;
            Comment = forum.Comment;
            CreationDate = forum.CreationDate;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ForumOwner.Id.ToString(),
                Location.Id.ToString(),
                Comment,
                CreationDate.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ForumOwner.Id = int.Parse(values[1]);
            Location.Id = int.Parse(values[2]);
            Comment = values[3];
            CreationDate = DateTime.Parse(values[4]);
        }
        public override string? ToString()
        {
            return $"{Location}";
        }
    }
}
