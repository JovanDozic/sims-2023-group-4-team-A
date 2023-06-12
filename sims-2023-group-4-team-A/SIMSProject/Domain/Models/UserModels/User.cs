using System;

namespace SIMSProject.Domain.Models.UserModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime Birthday { get; set; }
        public string Name { get; set; } = "UNESI";
        public string LastName { get; set; } = "UNESI";
        public string Email { get; set; } = "UNESI";

        public User()
        {
        }

        public static UserRole GetRole(string role)
        {
            return role switch
            {
                "Vlasnik" => UserRole.Owner,
                "Vodič" => UserRole.Guide,
                "Gost" => UserRole.Guest2,
                "Gost " => UserRole.Guest1,
                "Super Vlasnik" => UserRole.SuperOwner,
                "Super Vodič" => UserRole.SuperGuide,
                _ => UserRole.SuperGuest
            };
        }

        public static string GetRole(UserRole role)
        {
            return role switch
            {
                UserRole.Owner => "Vlasnik",
                UserRole.Guide => "Vodič",
                UserRole.Guest2 => "Gost",
                UserRole.Guest1 => "Gost ",
                UserRole.SuperOwner => "Super Vlasnik",
                UserRole.SuperGuide => "Super Vodič",
                _ => "Super Gost"
            };
        }
    }
}