using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.FileHandlers.AccommodationFileHandlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    public class AccommodationRenovationRepo : IAccommodationRenovationRepo
    {
        private readonly AccommodationRenovationFileHandler _fileHandler;
        private readonly IAccommodationRepo _accommodationRepo;
        private List<AccommodationRenovation> _renovations;

        public AccommodationRenovationRepo(IAccommodationRepo accommodationRepo)
        {
            _fileHandler = new();
            _renovations = new();
            _accommodationRepo = accommodationRepo;

            Load();
        }

        public void Load()
        {
            _renovations = _fileHandler.Load();

            MapAccommodations();
            CheckRenovationStatuses();
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _renovations;
        }

        public List<AccommodationRenovation> GetAllByAccommodationId(int accommodationId)
        {
            return _renovations.FindAll(x => x.Accommodation.Id == accommodationId);
        }

        public AccommodationRenovation GetById(int renovationId)
        {
            return _renovations.Find(x => x.Id == renovationId);
        }

        private void CheckRenovationStatuses()
        {
            var accommodations = _accommodationRepo.GetAll();
            foreach (var renovation in _renovations)
            {
                bool isInFuture = renovation.StartDate >= DateTime.Now;
                var accommodation = accommodations.Find(x => x.Id == renovation.Accommodation.Id) ?? new();
                bool isInRenovationPeriod = renovation.StartDate <= DateTime.Now && renovation.EndDate >= DateTime.Now;
                accommodation.IsInRenovation = isInRenovationPeriod && !isInFuture;
                accommodation.IsRecentlyRenovated = !isInRenovationPeriod && renovation.EndDate.AddYears(1) >= DateTime.Now && !isInFuture;
            }
            _accommodationRepo.SaveAll(accommodations);
        }

        private void MapAccommodations()
        {
            foreach (var renovation in _renovations)
            {
                renovation.Accommodation = _accommodationRepo.GetById(renovation.Accommodation.Id);
            }
        }

        public int NextId()
        {
            return _renovations.Count > 0 ? _renovations.Max(x => x.Id) + 1 : 1;
        }

        public AccommodationRenovation Save(AccommodationRenovation renovation)
        {
            renovation.Id = NextId();
            _renovations.Add(renovation);
            _fileHandler.Save(_renovations);
            return renovation;
        }

        public void SaveAll(List<AccommodationRenovation> renovations)
        {
            _fileHandler.Save(renovations);
            _renovations = renovations;
        }

        public void Update(AccommodationRenovation renovation)
        {
            AccommodationRenovation renovationToUpdate = GetById(renovation.Id) ?? throw new Exception("Updating Accommodation renovation failed!");
            int index = _renovations.IndexOf(renovationToUpdate);
            _renovations[index] = renovation;
            _fileHandler.Save(_renovations);
        }
    }
}
