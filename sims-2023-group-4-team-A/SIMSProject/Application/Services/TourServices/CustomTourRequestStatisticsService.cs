using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class CustomTourRequestStatisticsService
    {
        private readonly ICustomTourRequestRepo _customTourRequestRepo;

        public CustomTourRequestStatisticsService(ICustomTourRequestRepo customTourRequestRepo)
        {
            _customTourRequestRepo = customTourRequestRepo;
        }
        public double AcceptedRequestPercentageByGuestId(int guestId, int year)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Count;
            if (acceptedRequests == 0) return 0;
            return (acceptedRequests /= _customTourRequestRepo.GetAllByGuestId(guestId).FindAll(x => x.RequestCreateDate.Year == year).Count) * 100;
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
            if (acceptedRequests == 0) return 0;
            return guestcount / acceptedRequests;
        }
        public double AllTimeAverageGuestsInAcceptedRequests(int guestId)
        {
            double acceptedRequests = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).Count;
            double guestcount = _customTourRequestRepo.GetAllAcceptedByGuestId(guestId).Sum(x => x.GuestCount);
            if (acceptedRequests == 0) return 0;
            return guestcount / acceptedRequests;
        }
        
        public List<string> GetTourLanguages(int guestId, int year)
        {
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).ToList();
            if (year != -1) requests = requests.Where(r => r.RequestCreateDate.Year == year).ToList();
            List<string> languages = requests.Select(r => r.TourLanguage.ToString()).Distinct().ToList();
            return languages;
        }
        
        public List<string> GetTourLocations(int guestId, int year)
        {
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).ToList();
            if (year != -1) requests = requests.Where(r => r.RequestCreateDate.Year == year).ToList();
            List<string> locations = requests.Select(r => r.Location.ToString()).Distinct().ToList();
            return locations;
        }
        
        public Dictionary<string, int> GetRequestCountByLanguage(int guestId, int year)
        {
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).ToList();
            if (year != -1) requests = requests.Where(r => r.RequestCreateDate.Year == year).ToList();
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
            List<CustomTourRequest> requests = _customTourRequestRepo.GetAllByGuestId(guestId).ToList();
            if (year != -1) requests = requests.Where(r => r.RequestCreateDate.Year == year).ToList();
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
