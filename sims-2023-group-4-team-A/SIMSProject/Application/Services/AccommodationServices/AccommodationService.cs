using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationService
    {
        private readonly IAccommodationRepo _repo;
        private readonly AccommodationReservationService _accommodationReservationService;

        public AccommodationService(IAccommodationRepo repo)
        {
            _repo = repo;
            _accommodationReservationService = Injector.GetService<AccommodationReservationService>();
        }

        public void ReloadAccommodations()
        {
            _repo.Load();
        }

        public List<Accommodation> GetAllByOwnerId(int ownerId)
        {
            return _repo.GetAllByOwnerId(ownerId);
        }
        public List<Accommodation> GetAll()
        {
            return _repo.GetAll();
        }

        public void RegisterAccommodation(Accommodation accommodation)
        {
            _repo.Save(accommodation);
        }

        public int CountAllByOwnerId(int ownerId)
        {
            return _repo.GetAllByOwnerId(ownerId).Count;
        }
        public void Search(ObservableCollection<Accommodation> accommodations, string nameTypeLocation, int durationSearch, int maxGuestsSearch)
        {
            accommodations.Clear();
            foreach(var acc in new ObservableCollection<Accommodation>(_repo.GetAll()))
                accommodations.Add(acc);

            if (nameTypeLocation == "Naziv/Tip/Lokacija") nameTypeLocation = string.Empty;
            string[] searchValues = nameTypeLocation.Split(" ");
            List<Accommodation> searchResults = accommodations.ToList();

            foreach (string value in searchValues)
                searchResults.RemoveAll(x => !x.ToString().ToLower().Contains(value.ToLower()));

            // Removing by numbers
            if (durationSearch > 0) searchResults.RemoveAll(x => x.MinReservationDays > durationSearch);
            if (maxGuestsSearch > 0) searchResults.RemoveAll(x => x.MaxGuestNumber < maxGuestsSearch);


            accommodations.Clear();
            foreach (var searchResult in searchResults)
                accommodations.Add(searchResult);
        }
        public AccommodationReservation CheckReservations(List<AccommodationReservation> reservations, DateTime startDate, DateTime endDate, int accommodationId)
        {
            var conflictingReservation = reservations.FirstOrDefault(r => r.Accommodation.Id == accommodationId && (startDate < r.EndDate && r.StartDate < endDate));

            return conflictingReservation; //if return value is null it means that accommodation is free in specific date range
        }
       
    }
}
