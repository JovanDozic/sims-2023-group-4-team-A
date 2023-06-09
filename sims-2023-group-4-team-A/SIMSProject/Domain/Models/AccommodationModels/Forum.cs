using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class Forum: ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public int CommentsCount { get => Comments.Count; }
        public DateTime CreationDate { get; set; }
        public bool IsUseful { get; set; } = false;
        public bool IsClosed { get; set; } = false;

        public Forum()
        {
        }

        public static string CommentsToCSV(List<Comment> comments)
        {
            return comments.Count > 0 ? string.Join(",", comments.Select(x => x.Id)) : string.Empty;
        }

        public static List<Comment> CommentsFromCSV(string value)
        {
            var commentIds = value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            return commentIds.Select(x => new Comment() { Id = int.Parse(x) }).ToList();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Location.Id.ToString(),
                CommentsToCSV(Comments),
                CreationDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                IsUseful.ToString(),
                IsClosed.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            Location.Id = int.Parse(values[i++]);
            Comments = CommentsFromCSV(values[i++]);
            CreationDate = DateTime.Parse(values[i++], CultureInfo.GetCultureInfo("sr-LATN"));
            IsUseful = bool.Parse(values[i++]);
            IsClosed = bool.Parse(values[i++]);
        }

        public override string? ToString()
        {
            return $"Forum <{Id}> for {Location} has {Comments.Count} comments. Started by user <{Comments.First().User.Id}>";
        }
    }
}
