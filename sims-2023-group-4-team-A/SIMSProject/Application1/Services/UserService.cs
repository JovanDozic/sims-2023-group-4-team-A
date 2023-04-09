using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.Model.UserModel;
using System;
using System.Linq;

namespace SIMSProject.Application1.Services
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
            var user = GetByUsername(username) as User;

            if (user == null) throw new Exception("Korisnik ne postoji!");
            if (user.Password != password) throw new Exception("Pogrešna šifra!");

            switch (user.Role)
            {
                case "Vlasnik":
                    return user as Owner;
                case "Vodič":
                    return user as Guide;
                case "Gost":
                    return user as Guest;
            }
            return null;
        }


        public object? GetByUsername(string username)
        {
            var owner = _repo.GetAllOwners().FirstOrDefault(x => x.Username == username);
            if (owner != null)
            {
                return owner;
            }

            var guest = _repo.GetAllGuests().FirstOrDefault(x => x.Username == username);
            if (guest != null)
            {
                return guest;
            }

            var guide = _repo.GetAllGuides().FirstOrDefault(x => x.Username == username);
            if (guide != null)
            {
                return guide;
            }

            return null;
        }

    }
}
