using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class Forum: ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public DateTime CreationDate { get; set; }
        public bool IsClosed { get; set; } = false;
        public bool IsUseful { get; set; } = false;

        public Forum()
        {
        }
        public static string CommentsToCSV(List<Comment> comments)
        {
            return comments.Count > 0 ? string.Join(",", comments.Select(x => x.Id)) : string.Empty;
        }

        public static List<string> CommentsFromCSV(string value)
        {
            return value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Location.Id.ToString(),
                CommentsToCSV(Comments),
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
