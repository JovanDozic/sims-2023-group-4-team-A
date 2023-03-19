using SIMSProject.Model.UserModel;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace SIMSProject.Model
{
    public enum Status { ACTIVE = 0, INACTIVE, COMPLETED }
    public class TourDate : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        private int _tourId;
        public int TourId
        {
            get => _tourId;
            set
            {
                if (_tourId != value)
                {
                    _tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }

        private Status _tourStatus;
        public string TourStatus
        {
            get => _tourStatus switch
            {
                Status.ACTIVE => "Aktivna",
                Status.INACTIVE => "Neaktivna",
                _ => "Završena"
            };
            set => _tourStatus = value switch
            {
                "Aktivna" => Status.ACTIVE,
                "Neaktivna" => Status.INACTIVE,
                _ => Status.COMPLETED
            };
        }

        private int _currentKeyPointId;
        public int CurrentKeyPointId
        {
            get => _currentKeyPointId;
            set
            {
                if (value != _currentKeyPointId)
                {
                    _currentKeyPointId = value;
                    OnPropertyChanged(nameof(CurrentKeyPointId));
                }
            }
        }
        private int _availableSpots;
        public int AvailableSpots
        {
            get => _availableSpots;
            set
            {
                if (_availableSpots != value)
                {
                    _availableSpots = value;
                    OnPropertyChanged(nameof(AvailableSpots));
                }
            }
        }


        public Tour Tour { get; set; } = new();
        public KeyPoint CurrentKeyPoint { get; set; } = new();
        public List<Guest> Guests { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourDate() { }

        public TourDate(int id, DateTime date, int tourId, int availableSpots, int currentKeyPointId)
        {
            Id = id;
            Date = date;
            TourId = tourId;
            AvailableSpots = availableSpots;
            CurrentKeyPointId = currentKeyPointId;
        }

        public override string ToString()
        {
            return $"{Date}";
        }



        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Date = DateTime.Parse(values[1]);
            TourId = Convert.ToInt32(values[2]);
            TourStatus = values[3];
            AvailableSpots = Convert.ToInt32(values[4]);
            CurrentKeyPointId = Convert.ToInt32(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),
                Date.ToString("dd.MM.yyyy HH:mm:ss"),
                TourId.ToString(),
                TourStatus,
                AvailableSpots.ToString(),
                CurrentKeyPointId.ToString() };
            return csvValues;
        }
    }
}
