using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandlers.TourFileHandlers;
using SIMSProject.WPF.Views.Guest2Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.TourRepositories
{
    public class CustomTourRequestRepo : ICustomTourRequestRepo
    {
        public readonly CustomTourRequestFileHandler _fileHandler;
        private readonly IGuest2Repo _guestRepo;
        private readonly ILocationRepo _locationRepo;
        private List<CustomTourRequest> _customTourRequests;

        public CustomTourRequestRepo(IGuest2Repo guestRepo, ILocationRepo locationRepo)
        {
            _fileHandler = new CustomTourRequestFileHandler();
            _customTourRequests = _fileHandler.Load();
            _guestRepo = guestRepo;
            _locationRepo = locationRepo;
            MapGuests();
            MapLocations();
        }
        

        public List<CustomTourRequest> GetAll()
        {
            return _customTourRequests;
        }

        public List<CustomTourRequest> GetAllAcceptedByGuestId(int guestId)
        {
            return GetAllByGuestId(guestId).FindAll(x => x.RequestStatus == RequestStatus.ACCEPTED);
        }

        public List<CustomTourRequest> GetAllByGuestId(int guestId)
        {
            return _customTourRequests.FindAll(x => x.Guest.Id == guestId);
        }

        public CustomTourRequest GetById(int id)
        {
            return _customTourRequests.Find(x => x.Id == id);
        }
        
        public List<Language> GetRequestsLanguages()
        {
            return _customTourRequests.Select(x => x.TourLanguage).Distinct().ToList();
        }

        public List<Location> GetRequestsLocations()
        {
            return _customTourRequests.Select(x => x.Location).Distinct().ToList();
        }

        public int NextId()
        {
            return _customTourRequests.Count > 0 ? _customTourRequests.Max(x => x.Id) + 1 : 1; 
        }

        public CustomTourRequest Save(CustomTourRequest customTourRequest)
        {
            customTourRequest.Id = NextId();
            _customTourRequests.Add(customTourRequest);
            _fileHandler.Save(_customTourRequests);
            return customTourRequest;
        }

        public void SaveAll(List<CustomTourRequest> customTourRequests)
        {
            _fileHandler.Save(customTourRequests);
            _customTourRequests = customTourRequests;
        }

        public List<int> CountRequests(Location location)
        {
            var filteredRequests = _customTourRequests.Where(r => r.Location.Id == location.Id);
            return CountAnnualy(filteredRequests);
        }
        public List<int> CountRequests(Language language)
        {
            var filteredRequests = _customTourRequests.Where(r => r.TourLanguage == language);
            return CountAnnualy(filteredRequests);
        }
        public List<int> CountRequestsMonthly(Language language, int desiredYear)
        {
            var filteredRequests = _customTourRequests.Where(r => r.TourLanguage == language && r.RequestCreateDate.Year.Equals(desiredYear));
            return CountMonthly(filteredRequests);
        }

        public List<int> CountRequestsMonthly(Location location, int desiredYear)
        {
            var filteredRequests = _customTourRequests.Where(r => r.Location.Id == location.Id && r.RequestCreateDate.Year.Equals(desiredYear));
            return CountMonthly(filteredRequests);
        }
        private void MapGuests()
        {
            foreach(var customTourRequest  in _customTourRequests)
            {
                customTourRequest.Guest = _guestRepo.GetById(customTourRequest.Guest.Id);
            }
        }
        private void MapLocations()
        {
            foreach (var customTourRequest in _customTourRequests)
            {
                customTourRequest.Location = _locationRepo.GetById(customTourRequest.Location.Id);
            }
        }
        private static List<int> CountMonthly(IEnumerable<CustomTourRequest> filteredRequests)
        {
            var groupedRequests = filteredRequests.GroupBy(r => r.RequestCreateDate.Month);
            return Enumerable.Range(1, 12)
                .Select(month => groupedRequests.SingleOrDefault(g => g.Key == month)?.Count() ?? 0)
                .ToList();
        }
        private List<int> CountAnnualy(IEnumerable<CustomTourRequest> filteredRequests)
        {
            var groupedRequests = filteredRequests.GroupBy(r => r.RequestCreateDate.Year);
            return _customTourRequests.Select(x => x.RequestCreateDate.Year).Distinct()
                .OrderBy(x => x)
                .Select(year => groupedRequests.SingleOrDefault(g => g.Key == year)?.Count() ?? 0)
                .ToList();
        }

        public List<Location> GetMostWantedLocations()
        {
            var filteredRequests = _customTourRequests.Where(x => DateTime.Compare(x.RequestCreateDate, DateTime.Now.AddYears(-1)) > 0);
            var groupedRequests = filteredRequests.GroupBy(x => x.Location);
            var countedRequests = GetRequestsLocations()
                .Select(x =>
                {
                    var group = groupedRequests.SingleOrDefault(g => g.Key.Id == x.Id);
                    return new { Location = x, Count = group?.Count() ?? 0 };
                });
            var maxCount = countedRequests.Max(x => x.Count);
            return countedRequests.Where(x => x.Count == maxCount).Select(x => x.Location).ToList();
        }

        public List<Language> GetMostWantedLanguages()
        {
            var filteredRequests = _customTourRequests.Where(x => DateTime.Compare(x.RequestCreateDate, DateTime.Now.AddYears(-1)) > 0);
            var groupedRequests = filteredRequests.GroupBy(x => x.TourLanguage);
            var countedRequests = GetRequestsLanguages()
                .Select(x =>
                {
                    var group = groupedRequests.SingleOrDefault(g => g.Key == x);
                    return new { TourLanguage = x, Count = group?.Count() ?? 0 };
                });
            var maxCount = countedRequests.Max(x => x.Count);
            return countedRequests.Where(x => x.Count == maxCount).Select(x => x.TourLanguage).ToList();
        }

        public List<CustomTourRequest> GetOnHold()
        {
            return _customTourRequests.Where(x => x.RequestStatus == RequestStatus.ONHOLD).ToList();
        }

        public List<CustomTourRequest> GetAllSimilarRequests(Tour tour)
        {
            return GetAll().FindAll(x => (x.Location.City == tour.Location.City && x.Location.Country == x.Location.Country) || x.TourLanguage == tour.TourLanguage && x.RequestStatus != RequestStatus.ACCEPTED);
        }
    }
}
