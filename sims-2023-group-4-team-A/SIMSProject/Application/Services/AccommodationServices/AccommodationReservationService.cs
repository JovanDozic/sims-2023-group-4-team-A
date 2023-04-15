using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System.Collections.Generic;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepo _repo;

        public AccommodationReservationService(IAccommodationReservationRepo repo)
        {
            _repo = repo;
        }

        public List<AccommodationReservation> GetAll()
        {
            return _repo.GetAll();
        }

        public List<AccommodationReservation> GetAllByAccommodationId(int accommodationId)
        {
            return _repo.GetAllByAccommodationId(accommodationId);
        }

        public void UpdateReservation(AccommodationReservation selectedReservation)
        {
            _repo.Update(selectedReservation);
        }
    }
}
