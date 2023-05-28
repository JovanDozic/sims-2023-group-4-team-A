using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System;
using System.Linq;
using SIMSProject.Domain.Models;

namespace SIMSProject.Application.Services.UserServices
{
    public class UserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        public object? GetUser(string username, string password)
        {
            if (GetByUsername(username) is not User user) throw new Exception("Korisnik ne postoji!");

            if (user.Password != password) throw new Exception("Pogrešna šifra!");

            return user.Role switch
            {
                UserRole.Owner or UserRole.SuperOwner => user,
                UserRole.Guide or UserRole.SuperGuide => user as Guide,
                UserRole.Guest1 or UserRole.SuperGuest => user as Guest1,
                UserRole.Guest2 => user as Guest2,
                _ => null,
            };
        }

        public object? GetByUsername(string username)
        {
            return _repo.GetAllOwners().FirstOrDefault(x => x.Username == username) as object
                ?? _repo.GetAllGuests().FirstOrDefault(x => x.Username == username) as object
                ?? _repo.GetAllGuests1().FirstOrDefault(x => x.Username == username) as object
                ?? _repo.GetAllGuides().FirstOrDefault(x => x.Username == username);
        }
    }
}
