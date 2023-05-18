using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IAccommodationRenovationRepo
    {
        public void Load();
        public AccommodationRenovation GetById(int renovationId);
        public List<AccommodationRenovation> GetAllByAccommodationId(int accommodationId);
        public List<AccommodationRenovation> GetAll();
        public int NextId();
        public AccommodationRenovation Save(AccommodationRenovation renovation);
        public void SaveAll(List<AccommodationRenovation> renovations);
        public void Update(AccommodationRenovation renovation);
    }
}
