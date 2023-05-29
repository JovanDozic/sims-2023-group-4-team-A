using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.UserRepositories
{
    public class UserRepo : IUserRepo
    {
        private readonly IOwnerRepo _ownerRepo;
        private readonly IOwnerRatingRepo _ownerRatingRepo;
        
        private readonly IGuideRepo _guideRepo;
        private readonly IGuideRatingRepo _guideRatingRepo;

        private readonly IGuest2Repo _guestRepo;
        private readonly IGuestRatingRepo _guestRatingRepo;

        private readonly IGuest1Repo _guest1Repo;

        public UserRepo(IOwnerRepo ownerRepo, IOwnerRatingRepo ownerRatingRepo, 
                        IGuideRepo guideRepo, IGuideRatingRepo guideRatingRepo,
                        IGuest2Repo guestRepo, IGuestRatingRepo guestRatingRepo, IGuest1Repo guest1Repo)
        {
            _ownerRepo = ownerRepo;
            _ownerRatingRepo = ownerRatingRepo;

            _guideRepo = guideRepo;
            _guideRatingRepo = guideRatingRepo;

            _guestRepo = guestRepo;
            _guestRatingRepo = guestRatingRepo;

            _guest1Repo = guest1Repo;

            CalculateRatings();
        }

        private void CalculateRatings()
        {
            _ownerRepo.SaveAll(GetAllOwners().Select(x =>
                {
                    x.Rating = _ownerRatingRepo.GetOverallByOwnerId(x.Id);
                    return x;
                }
            ).ToList());

            _guideRepo.SaveAll(GetAllGuides().Select(x =>
            {
                x.Rating = _guideRatingRepo.GetOverallByGuideId(x.Id);
                return x;
            }
            ).ToList());

            _guest1Repo.SaveAll(GetAllGuests1().Select(x =>
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
                UserRole.Guest1 or UserRole.SuperGuest => GetAllGuests1().Find(x => x.Id == id),
                UserRole.Guest2 => GetAllGuests().Find(x => x.Id == id),
                _ => GetAllGuides().Find(x => x.Id == id)
            };
        }

        public List<Guest2> GetAllGuests()
        {
            return _guestRepo.GetAll();
        }

        public List<Guest1> GetAllGuests1()
        {
            return _guest1Repo.GetAll();
        }
        public List<Guide> GetAllGuides()
        {
            return _guideRepo.GetAll();
        }

        public List<Owner> GetAllOwners()
        {
            return _ownerRepo.GetAll();
        }

        public Guest2? GetGuestById(int id)
        {
            return _guestRepo.GetById(id);
        }
        public Guest1? GetGuest1ById(int id)
        {
            return _guest1Repo.GetById(id);
        }

        public Guide? GetGuideById(int id)
        {
            return _guideRepo.GetById(id);
        }

        public Owner? GetOwnerById(int id)
        {
            return _ownerRepo.GetById(id);
        }
    }
}
