using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IAccommodationRepo
    {
        public void Load();
        public Accommodation GetById(int accommodationId);
        public List<Accommodation> GetAllByOwnerId(int ownerId);
        public List<Accommodation> GetAll();
        public int NextId();
        public Accommodation Save(Accommodation accommodation);
        public void SaveAll(List<Accommodation> accommodations);
    }
}
