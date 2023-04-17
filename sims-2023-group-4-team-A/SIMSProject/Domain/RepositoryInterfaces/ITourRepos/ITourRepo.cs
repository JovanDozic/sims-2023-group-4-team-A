using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.ITourRepos
{
    public interface ITourRepo
    {
        public int NextId();
        public List<Tour> GetAll();
        public Tour GetById(int id);
        public Tour Save(Tour tour);
        public void SaveAll(List<Tour> tours);
        public List<Tour> FindTodaysTours();
        public List<Tour> GetToursWithSameLocation(Tour selectedTour);
        public void SearchTours(string locationAndLanguage, int searchDuration, int searchMaxGuests, ObservableCollection<Tour> tours);
    }
}
