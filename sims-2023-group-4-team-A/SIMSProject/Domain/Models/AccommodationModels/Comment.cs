using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Globalization;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; } = new();
        public bool WasAtLocation { get; set; } = true;
        public string Text { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int Downvotes { get; set; } = 0;

        public Comment()
        {
        }

        public Comment(Comment original)
        {
            Id = original.Id;
            User = original.User;
            WasAtLocation = original.WasAtLocation;
            Text = original.Text;
            CreationDate = original.CreationDate;
            Downvotes = original.Downvotes;
            UserDownvoted = original.UserDownvoted;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                User.Id.ToString(),
                User.GetRole(User.Role),
                (User.Role == UserRole.Owner || User.Role == UserRole.SuperOwner) ? "True" : WasAtLocation.ToString(),
                Text,
                CreationDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                Downvotes.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            User.Id = int.Parse(values[i++]);
            User.Role = User.GetRole(values[i++]);
            WasAtLocation = bool.Parse(values[i++]);
            Text = values[i++];
            CreationDate = DateTime.Parse(values[i++], CultureInfo.GetCultureInfo("sr-LATN"));
            Downvotes = int.Parse(values[i++]);
        }

        public string CommentUserIcon
        {
            get
            {
                if (User.Role == UserRole.Guest1 || User.Role == UserRole.SuperGuest) return WasAtLocation ? "WasAtLocationIcon" : "WasNotAtLocationIcon";
                if (User.Role == UserRole.Owner) return "IsOwnerIcon";
                if (User.Role == UserRole.SuperOwner) return "IsSuperOwnerIcon";
                return "QuestionIcon";
            }
        }

        public bool UserDownvoted { get; set; } = false;
        public string UserDownvotedIcon
        {
            get
            {
                if (UserDownvoted) return "DownvoteFillIcon";
                return "DownvoteIcon";
            }
        }
    }
}