using Dynamitey.DynamicObjects;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Application.Services.TourServices
{
    public class CustomTourRequestService
    {
        private readonly ICustomTourRequestRepo _customTourRequestRepo;

        public CustomTourRequestService(ICustomTourRequestRepo customTourRequestRepo)
        {
            _customTourRequestRepo = customTourRequestRepo;
        }
        public void Save(CustomTourRequest customTourRequest)
        {
            _customTourRequestRepo.Save(customTourRequest);
        }
        public List<CustomTourRequest> GetAllByGuestId(int guestId)
        {
            return _customTourRequestRepo.GetAllByGuestId(guestId);
        }

        public List<CustomTourRequest> GetAll()
        {
            return _customTourRequestRepo.GetAll();
        }

        public List<Location> GetRequestsLocations()
        {
            return _customTourRequestRepo.GetRequestsLocations();
        }

        public List<Language> GetRequestsLanguages()
        {
            return _customTourRequestRepo.GetRequestsLanguages();
        }

        public List<CustomTourRequest> FilterRequests(Location location, Language language, int numOfGuests, DateTime start, DateTime end)
        {
            List<CustomTourRequest> requests = new(_customTourRequestRepo.GetAll());
            if (location.Id != 0)
            {
                requests.RemoveAll(x => x.Location.Id != location.Id);
            }
            if (language > 0)
            {
                requests.RemoveAll(x => !x.TourLanguage.Equals(language));
            }
            if (numOfGuests > 0)
            {
                requests.RemoveAll(x => x.GuestCount != numOfGuests);
            }
            if(DateTime.Compare(start, end) != 0)
            {
                requests.RemoveAll(x => DateTime.Compare(x.StartDate, start) < 0 || DateTime.Compare(x.EndDate, end) > 0);
            }
            return requests;
        }
        public void CheckRequestValidity(List<CustomTourRequest> customTourRequests)
        {
            foreach (var customRequest in customTourRequests)
            {
                if((customRequest.StartDate-DateTime.Now).TotalHours <= 48)
                {
                    customRequest.RequestStatus = RequestStatus.INVALID;
                }
            }
            _customTourRequestRepo.SaveAll(_customTourRequestRepo.GetAll());
        }

        public double AcceptedRequestPercentageByGuestId(int guestId, int year)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).FindAll(x=>x.RequestCreateDate.Year==year).Count;
            if (acceptedRequests == 0) return 0;
            return (acceptedRequests /= _customTourRequestRepo.GetAllByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Count)*100;
        }
        public double AllTimeAcceptedRequestPercentageByGuestId(int guestId)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).Count;
            if (acceptedRequests == 0) return 0;
            return (acceptedRequests /= _customTourRequestRepo.GetAllByGuestId(guestId).Count) * 100;
        }
        public double AverageGuestsInAcceptedRequests(int guestId, int year)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Count;
            double guestcount = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Sum(x => x.GuestCount);
            if(acceptedRequests == 0) return 0;
            return guestcount/acceptedRequests;
        }
        public double AllTimeAverageGuestsInAcceptedRequests(int guestId)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).Count;
            double guestcount = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).Sum(x => x.GuestCount);
            if (acceptedRequests == 0) return 0;
            return guestcount / acceptedRequests;
        }

        public void ApproveRequest(CustomTourRequest request)
        {
            CustomTourRequest old = _customTourRequestRepo.GetById(request.Id);
            old.RequestStatus = RequestStatus.ACCEPTED;
            _customTourRequestRepo.SaveAll(_customTourRequestRepo.GetAll());
        }

        public List<int> CountRequests(Location location)
        {
            return _customTourRequestRepo.CountRequests(location);
        }

        public List<int> CountRequests(Language language)
        {
            return _customTourRequestRepo.CountRequests(language);
        }
        public List<int> CountRequestsMonthly(Location location, int desiredYear)
        {
            return _customTourRequestRepo.CountRequestsMonthly(location, desiredYear);
        }
        public List<int> CountRequestsMonthly(Language language, int desiredYear)
        {
            return _customTourRequestRepo.CountRequestsMonthly(language, desiredYear);
        }

        public List<int> GetRequestsYears()
        {
            return _customTourRequestRepo.GetAll().Select(x => x.StartDate.Year).Distinct().OrderBy(x => x).ToList();
        }

        public List<string> GetTourLanguagesByYear(int guestId, int year)
        {
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).Where(r =>r.RequestCreateDate.Year == year).ToList();
            // Dohvati jedinstvene jezike iz svih zahteva za ture
            List<string> languages = requests.Select(r => r.TourLanguage.ToString()).Distinct().ToList();
            return languages;
        }
        public List<string> GetTourLanguages(int guestId)
        {
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).ToList();
            // Dohvati jedinstvene jezike iz svih zahteva za ture
            List<string> languages = requests.Select(r => r.TourLanguage.ToString()).Distinct().ToList();
            return languages;
        }
        public List<string> GetTourLocationsByYear(int guestId, int year)
        {
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).Where(r => r.RequestCreateDate.Year == year).ToList();
            // Dohvati jedinstvene lokacije iz svih zahteva za ture
            List<string> locations = requests.Select(r => r.Location.ToString()).Distinct().ToList();
            return locations;
        }
        public List<string> GetTourLocations(int guestId)
        {
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).ToList();
            // Dohvati jedinstvene lokacije iz svih zahteva za ture
            List<string> locations = requests.Select(r => r.Location.ToString()).Distinct().ToList();
            return locations;
        }
        public Dictionary<string, int> GetRequestCountByLanguage(int guestId, int year)
        {
            // Dohvati sve zahteve za ture za određenog gosta i godinu
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).Where(r => r.RequestCreateDate.Year == year).ToList();

            // Izračunaj broj zahtjeva za svaki jezik
            Dictionary<string, int> requestCounts = new Dictionary<string, int>();

            foreach (CustomTourRequest request in requests)
            {
                string language = request.TourLanguage.ToString();

                if (requestCounts.ContainsKey(language))
                {
                    requestCounts[language]++;
                }
                else
                {
                    requestCounts[language] = 1;
                }
            }
            return requestCounts;
        }

        public Dictionary<string, int> GetAllTimeRequestCountByLanguage(int guestId)
        {
            // Dohvati sve zahteve za ture za određenog gosta i godinu
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).ToList();

            // Izračunaj broj zahtjeva za svaki jezik
            Dictionary<string, int> requestCounts = new Dictionary<string, int>();

            foreach (CustomTourRequest request in requests)
            {
                string language = request.TourLanguage.ToString();

                if (requestCounts.ContainsKey(language))
                {
                    requestCounts[language]++;
                }
                else
                {
                    requestCounts[language] = 1;
                }
            }
            return requestCounts;
        }

        public Dictionary<string, int> GetRequestCountByLocation(int guestId, int year)
        {
            // Dohvati sve zahteve za ture za određenog gosta i godinu
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).Where(r => r.RequestCreateDate.Year == year).ToList();

            // Izračunaj broj zahtjeva za svaku lokaciju
            Dictionary<string, int> requestCounts = new Dictionary<string, int>();

            foreach (CustomTourRequest request in requests)
            {
                string location = request.Location.ToString();

                if (requestCounts.ContainsKey(location))
                {
                    requestCounts[location]++;
                }
                else
                {
                    requestCounts[location] = 1;
                }
            }

            return requestCounts;
        }
        public Dictionary<string, int> GetAllTimeRequestCountByLocation(int guestId)
        {
            // Dohvati sve zahteve za ture za određenog gosta i godinu
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).ToList();

            // Izračunaj broj zahtjeva za svaku lokaciju
            Dictionary<string, int> requestCounts = new Dictionary<string, int>();

            foreach (CustomTourRequest request in requests)
            {
                string location = request.Location.ToString();

                if (requestCounts.ContainsKey(location))
                {
                    requestCounts[location]++;
                }
                else
                {
                    requestCounts[location] = 1;
                }
            }

            return requestCounts;
        }
    }
}
