using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;

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
            User.Role = User.GetRole(values[i++]);
            WasAtLocation = bool.Parse(values[i++]);
            Text = values[i++];
            CreationDate = DateTime.Parse(values[i++]);
            Downvotes = int.Parse(values[i++]);
        }

        public string CommentUserIcon
        {
            get
            {
                if (User.Role == UserRole.Guest1 || User.Role == UserRole.SuperGuest) return WasAtLocation ? "/Resources/Icons/was-at-location.png" : "/Resources/Icons/was-not-at-location.png";
                if (User.Role == UserRole.Owner) return "/Resources/Icons/is-owner.png";
                if (User.Role == UserRole.SuperOwner) return "/Resources/Icons/is-superowner.png";
                return "/Resources/Icons/question.png";
            }
        }
        public bool UserDownvoted { get; set; } = false;

        public string UserDownvotedIcon
        {
            get
            {
                if (UserDownvoted) return "/Resources/Icons/downvote-fill.png";
                return "/Resources/Icons/downvote.png";
            }
        }
    }
}