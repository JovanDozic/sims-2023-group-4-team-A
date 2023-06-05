using Microsoft.VisualStudio.Services.Users;
using System;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class Comment
    {
        public int Id { get; set; }
        public User User { get; set; } = new();
        public string Text { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int Downvotes { get; set; } = 0;

        public Comment()
        {
        }


    }
}