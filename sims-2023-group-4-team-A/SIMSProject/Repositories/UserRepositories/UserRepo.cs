using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandler.UserFileHandler;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.UserRepositories
{
    public class UserRepo : IUserRepo
    {
        private readonly IOwnerRepo _ownerRepo;
        // TODO: IOwnerRatingRepo
        // TODO: private readonly IGuideRepo _guideRepo;
        private readonly IGuestRepo _guestRepo;
        private readonly IGuestRatingRepo _guestRatingRepo;

        public UserRepo(IOwnerRepo ownerRepo, /* TODO: Implement IGuideRepo */ IGuestRepo guestRepo, IGuestRatingRepo guestRatingRepo)
        {
            _ownerRepo = ownerRepo;
            // TODO: IOwnerRatingRepo
            _guestRepo = guestRepo;
            _guestRatingRepo = guestRatingRepo;

            CalculateRatings();
        }

        private void CalculateRatings()
        {
            _ownerRepo.SaveAll(GetAllOwners().Select(x =>
                {
                    // TODO: Calculate Owner rating
                    return x;
                }
            ).ToList());

            _guestRepo.SaveAll(GetAllGuests().Select(x =>
                {
                    x.Rating = _guestRatingRepo.GetOverallByGuestId(x.Id);
                    return x;
                }
            ).ToList());
        }

        public User GetByIdAndRole(int id, UserRole role)
        {
            return role switch
            {
                UserRole.Owner or UserRole.SuperOwner => GetAllOwners().Find(x => x.Id == id),
                UserRole.Guest or UserRole.SuperGuest => GetAllGuests().Find(x => x.Id == id),
                _ => GetAllGuides().Find(x => x.Id == id)
            };
        }

        public List<Guest> GetAllGuests()
        {
            return _guestRepo.GetAll();
        }

        public List<Guide> GetAllGuides()
        {
            // TODO: Fix this
            return new GuideFileHandler().Load();
        }

        public List<Owner> GetAllOwners()
        {
            return _ownerRepo.GetAll();
        }

        public Guest? GetGuestById(int id)
        {
            return _guestRepo.GetById(id);
        }

        public Guide? GetGuideById(int id)
        {
            // TODO: Fix this
            return new GuideFileHandler().Load().Find(x => x.Id == id);
        }

        public Owner? GetOwnerById(int id)
        {
            return _ownerRepo.GetById(id);
        }
    }
}
