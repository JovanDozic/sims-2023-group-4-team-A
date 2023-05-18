using Dynamitey.DynamicObjects;
using SIMSProject.Application.Services;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Application.Services.UserServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Messenger;
using SIMSProject.WPF.Messenger.Messages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels
{
    public class TourCreationViewModel : ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly LocationService _locationService;
        private readonly KeyPointService _keyPointService;
        private readonly TourReservationService _tourReservationService;
        private readonly NotificationService _notificationService;
        private readonly CustomTourRequestService _customTourRequestService;

        private bool _cbLocationIsEnabled = true;
        public bool CbLocationIsEnabled
        {
            get => _cbLocationIsEnabled;
            set
            {
                if (value == _cbLocationIsEnabled) return;
                _cbLocationIsEnabled = value;
                OnPropertyChanged(nameof(CbLocationIsEnabled));
            }
        }
        private bool _cbLanguageIsEnabled = true;
        public bool CbLanguageIsEnabled
        {
            get => _cbLanguageIsEnabled;
            set
            {
                if (value == _cbLanguageIsEnabled) return;
                _cbLanguageIsEnabled = value;
                OnPropertyChanged(nameof(CbLanguageIsEnabled));
            }
        }
        private bool _dpDateIsEnabled = true;
        public bool DpDateIsEnabled
        {
            get => _dpDateIsEnabled;
            set
            {
                if (value == _dpDateIsEnabled) return;
                _dpDateIsEnabled = value;
                OnPropertyChanged(nameof(DpDateIsEnabled));
            }
        }
        private TourReservation _tourReservation = new();
        private Tour _tour = new();
        public Tour Tour
        {
            get => _tour;
            set
            {
                if (value != _tour)
                {
                    _tour = value;
                    OnPropertyChanged(nameof(Tour));
                }
            }
        }
        public Guide Guide
        {
            get => _tour.Guide;
            set
            {
                if (_tour.Guide != value)
                {
                    _tour.Guide = value;
                    OnPropertyChanged(nameof(Guide));
                }

            }
        }
        public string Name
        {
            get { return _tour.Name; }
            set
            {
                if (_tour.Name != value)
                {
                    _tour.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Description
        {
            get { return _tour.Description; }
            set
            {
                if (value != _tour.Description)
                {
                    _tour.Description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private Language _language;
        public string TourLanguage
        {
            get => Tour.GetLanguage(_language);
            set
            {
                if (value == _language.ToString()) return;
                _language = value switch
                {
                    "Engleski" => Language.ENGLISH,
                    "Srpski" => Language.SERBIAN,
                    "Španski" => Language.SPANISH,
                    _ => Language.FRENCH
                };
                OnPropertyChanged(nameof(TourLanguage));
                _tour.TourLanguage = _language;
            }
        }
        public int MaxGuestNumber
        {
            get => _tour.MaxGuestNumber;
            set
            {
                if (value >= 1)
                {
                    _tour.MaxGuestNumber = value;
                    OnPropertyChanged(nameof(MaxGuestNumber));
                }
            }
        }
        public int Duration
        {
            get => _tour.Duration;
            set
            {
                if (value >= 1)
                {
                    _tour.Duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }
        private Location _location = new();
        public Location SelectedLocation
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    SelectedLocation.Id = value.Id;
                    OnPropertyChanged(nameof(SelectedLocation));
                    _tour.Location = _location;
                    Keys = new(_keyPointService.GetAll().Where(x => x.Location.Id == SelectedLocation.Id));
                }
            }
        }
        
        private KeyPoint _keyPoint = new();
        public KeyPoint SelectedKeyPoint
        {
            get => _keyPoint;
            set
            {
                if (value == _keyPoint) return;
                _keyPoint = value;
                OnPropertyChanged(nameof(SelectedKeyPoint));
            }
        }

        private string _imageUrl = string.Empty;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value == _imageUrl) return;
                _imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }
        
        private DateTime _appointment;
        public DateTime SelectedAppointment
        {
            get => _appointment;
            set
            {
                if (value == _appointment) return;
                _appointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }
        private int hours;
        public int Hours
        {
            get => hours;
            set
            {
                if (hours != value && value >= 0 && value <= 24)
                {
                    hours = value;
                    OnPropertyChanged(nameof(Hours));
                }

            }
        }
        private int minutes;
        public int Minutes
        {
            get => minutes;
            set
            {
                if (minutes != value && value >= 0 && value <= 60)
                {
                    minutes = value;
                    OnPropertyChanged(nameof(Hours));
                }

            }
        }

        private ObservableCollection<KeyPoint> _keyPoints = new();
        public ObservableCollection<KeyPoint> KeyPoints
        {
            get => _keyPoints;
            set
            {
                if (value == _keyPoints) return;
                _keyPoints = value;
                OnPropertyChanged(nameof(KeyPoints));
            }
        }
        private ObservableCollection<KeyPoint> _keys = new();
        public ObservableCollection<KeyPoint> Keys
        {
            get => _keys;
            set
            {
                if (value == _keys) return;
                _keys = value;
                OnPropertyChanged(nameof(Keys));
            }
        }
        private ObservableCollection<string> _images = new();
        public ObservableCollection<string> Images
        {
            get => _images;
            set
            {
                if (value == _images) return;
                _images = value;
                OnPropertyChanged(nameof(Images));
            }
        }
        private ObservableCollection<TourAppointment> _appointments = new();
        public ObservableCollection<TourAppointment> Appointments
        {
            get => _appointments;
            set
            {
                if (value != _appointments)
                {
                    _appointments = value;
                    OnPropertyChanged(nameof(Appointments));
                }
            }
        }
        public ObservableCollection<Location> AllLocations { get; set; } = new();
        public ObservableCollection<string> TourLanguages { get; set; }
        public Guest Guest { get; set; } = new();
        public TourCreationViewModel()
        {
            _tourService = Injector.GetService<TourService>();
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _locationService = Injector.GetService<LocationService>();
            _keyPointService = Injector.GetService<KeyPointService>();
            _tourReservationService = Injector.GetService<TourReservationService>();
            _notificationService = Injector.GetService<NotificationService>();
            _customTourRequestService = Injector.GetService<CustomTourRequestService>();

            AllLocations = new(_locationService.FindAll());
            TourLanguages = new(Tour.GetLanguages());

            AddAppointmentCommand = new RelayCommand(AddAppointmentExecute, AddAppointmentCanExecute);
            AddKeyPointCommand = new RelayCommand(AddKeyPointExecute, AddKeyPointCanExecute);
            AddImageCommand = new RelayCommand(AddImageExecute, AddImageCanExecute);
            CreateTourCommand = new RelayCommand(CreateTourExecute, CreateTourCanExecute);

            MessageBus.Subscribe<CreateRequestedMessage>(this, OpenMessage);
            MessageBus.Subscribe<CreateMostWantedMessage>(this, OpenMessage1);
        }
        private void OpenMessage1(CreateMostWantedMessage message)
        {
            Tour.Reason = CreatingReason.STATISTICS;
            if (message.Language > 0)
            {
                TourLanguage = Tour.GetLanguage(message.Language);
                CbLanguageIsEnabled = false;
                return;
            }
            SelectedLocation = message.Location;
            CbLocationIsEnabled = false;
        }
        private void OpenMessage(CreateRequestedMessage message)
        {
            SelectedLocation = message.Request.Location;
            TourLanguage = Tour.GetLanguage(message.Request.TourLanguage);
            MaxGuestNumber = message.Request.GuestCount;
            SelectedAppointment = message.Appointment;

            CbLanguageIsEnabled = false;
            CbLocationIsEnabled = false;
            DpDateIsEnabled = false;

            Tour.Reason = CreatingReason.CUSTOM;
            _tourReservation.GuestId = message.Request.Guest.Id;
            _tourReservation.GuestNumber = message.Request.GuestCount;
            Guest = message.Request.Guest;
        }

        #region AddKeyPointCommand
        public ICommand AddKeyPointCommand { get; private set; }
        public bool AddKeyPointCanExecute()
        {
            return SelectedKeyPoint.Id > 0;
        }
        public void AddKeyPointExecute()
        {
            KeyPoints.Add(SelectedKeyPoint);
        }
        #endregion
        #region AddAppointmentCommand
        public ICommand AddAppointmentCommand { get; private set; }
        public bool AddAppointmentCanExecute()
        {
            return MaxGuestNumber > 0;
        }
        public void AddAppointmentExecute()
        {
            DateTime newDate = new(SelectedAppointment.Year, SelectedAppointment.Month, SelectedAppointment.Day, Hours, Minutes, 0);
            Appointments.Add(new(newDate, -1, MaxGuestNumber, -1, Guide.Id));
        }
        #endregion
        #region AddImageCommand
        public ICommand AddImageCommand { get; private set; }
        public bool AddImageCanExecute()
        {
            return ImageUrl != string.Empty;
        }
        public void AddImageExecute()
        {
            Images.Add(ImageUrl);
            ImageUrl = string.Empty;
        }
        #endregion
        #region CreateTourCommand
        public ICommand CreateTourCommand { get; private set; }
        public bool CreateTourCanExecute()
        {
            return Images.Count > 0 && KeyPoints.Count > 1;
        }
        public void CreateTourExecute()
        {
            Tour.KeyPoints.AddRange(KeyPoints.ToList());
            Tour.Images.AddRange(Images.ToList());
            _tourService.CreateTour(Tour);
            _tourAppointmentService.CreateAppointments(Appointments.ToList(), Tour);
            SendNewTourNotification();
            if(Tour.Reason == CreatingReason.CUSTOM)
            {
                _tourReservation.TourAppointment = Appointments.FirstOrDefault();
                _tourReservationService.Save(_tourReservation);
                SendAcceptedTourNotification();
            }
        }
        private void SendAcceptedTourNotification()
        {
            string title = "Prihvaćen zahtev";
            string description = $"Vaš zahtev za turu je prihvaćen." +
                $" U pitanju je tura na lokaciji {_tour.Location}." +
                $" Jezik na kom će tura biti realizovana je {Tour.GetLanguage(_tour.TourLanguage)} u terminu " +
                $"{_tourReservation.TourAppointment.Date.ToString()}. " +
                $"Rezervaciju i detalje možete videti u listi svojih rezervacija.";
            
            var notification = new Notification(Guest, title, description);
            _notificationService.CreateNotification(notification);
        }
        private void SendNewTourNotification()
        {
            string title = "Nova tura";
            string description = $"Kreirana je nova tura koja bi mogla da Vas interesuje, ispunjava neke od dosad neispunjenih zahteva." +
                $" U pitanju je tura: {_tour.Name} na lokaciji {_tour.Location}." +
                $" Jezik na kom će tura biti realizovana je {Tour.GetLanguage(_tour.TourLanguage)}. ";
            if (Tour.Reason != CreatingReason.STATISTICS) return;
            foreach (Guest guest in _customTourRequestService.GetGuestsWithSimilarRequests(_tour))
            {
                var notification = new Notification(guest, title, description);
                _notificationService.CreateNotification(notification);
            }
        }
        #endregion
    }
}
