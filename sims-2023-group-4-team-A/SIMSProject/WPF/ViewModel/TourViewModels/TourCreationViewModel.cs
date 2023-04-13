using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using SIMSProject.View.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.WPF.ViewModel.TourViewModels
{
    public class TourCreationViewModel : BaseTourViewModel
    {
        private readonly TourService _tourService = new();
        private readonly TourAppointmentService _tourAppointmentService = new();
        private readonly TourKeyPointService  _tourKeyPointService = new();

        public new Location Location
        {
            get { return _tour.Location; }
            set
            {
                if (_tour.Location != value)
                {
                    _tour.Location = value;
                    LocationId = value.Id;
                    Keys.Clear();
                    foreach (var point in GuideInitialWindow.KeyPointController.GetAll().FindAll(x => x.LocationId == _tour.LocationId))
                    {
                        Keys.Add(point);
                    }
                    OnPropertyChanged(nameof(Location));
                }

            }
        }
        public List<string> TourLanguages { get; set; }
        public ObservableCollection<Location> AllLocations { get; set; } = new();
        public ObservableCollection<KeyPoint> Keys { get; set;} = new();
        public KeyPoint? SelectedKeyPoint { get; set; }
        public DateTime SelectedAppointment { get; set; } = DateTime.Now;

        public TourCreationViewModel(Guide guide) : base(new())
        {
            TourLanguages = new()
            {
                "Srpski",
                "Engleski",
                "Francuski",
                "Španski"
            };
            Guide = guide;
            AllLocations = new(GuideInitialWindow.LocationController.GetAll());
        }

        public void CreateTour()
        {
            _tourService.CreateTour(_tour);
            _tourAppointmentService.CreateAppointments(_tour.Appointments, _tour);
            _tourKeyPointService.CreateNewPairs(_tour);
            MessageBox.Show("Tura uspešno kreirana.");

        }

        public bool IsValid()
        {
            return KeyPoints.Count < 2 || Images.Count < 1;
        }

        public void AddKeyPoint()
        {
            if(SelectedKeyPoint == null)
            {
                MessageBox.Show("Nije moguće izabrati ključnu tačku koja ne postoji!");
                return;
            }
            KeyPoints.Add(SelectedKeyPoint);
        }
    }
}
