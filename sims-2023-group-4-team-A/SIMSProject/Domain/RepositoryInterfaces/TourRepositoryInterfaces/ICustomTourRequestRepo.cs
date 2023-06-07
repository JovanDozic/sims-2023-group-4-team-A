using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ICustomTourRequestRepo
    {
        public int NextId();
        public List<CustomTourRequest> GetAll();
        public List<CustomTourRequest> GetOnHold();
        public List<CustomTourRequest> GetAllByGuestId(int guestId);
        public List<CustomTourRequest> GetAllComplexTourPartsByGuestId(int guestId);
        public List<CustomTourRequest> GetAllAcceptedByGuestId(int guestId);
        public List<CustomTourRequest> GetAllSimilarRequests(Tour tour);
        public List<CustomTourRequest> GetAllComplexTourParts(int complexTourId);
        public CustomTourRequest Save(CustomTourRequest customTourRequest);
        public void SaveAll(List<CustomTourRequest> customTourRequests);
        public List<Location> GetRequestsLocations();
        public List<Language> GetRequestsLanguages();
        public CustomTourRequest GetById(int id);
        public List<int> CountRequests(Location location);
        public List<int> CountRequests(Language language);
        public List<int> CountRequestsMonthly(Location location, int desiredYear);
        public List<int> CountRequestsMonthly(Language language, int desiredYear);
        public List<Location> GetMostWantedLocations();
        public List<Language> GetMostWantedLanguages();
    }
}
