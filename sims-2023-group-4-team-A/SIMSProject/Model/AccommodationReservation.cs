using System;
using SIMSProject.Serializer;
using SIMSProject.Model.UserModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.Model
{
    public class AccommodationReservation: ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; } = new();
        public Guest Guest { get; set; } = new();
        private DateTime _startDate; 
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if(value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if(value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                } 
            }
        }
        private int _numberOfDays;
        public int NumberOfDays
        {
            get => _numberOfDays;
            set
            {
                if(value != _numberOfDays)
                {
                    _numberOfDays = value;
                    OnPropertyChanged(nameof(NumberOfDays));
                }
            }
        }
        private int _guestsNumber;
        public int GuestNumber
        {
            get => _guestsNumber;
            set
            {
                if(value != _guestsNumber)
                {
                    _guestsNumber = value;
                    OnPropertyChanged(nameof(GuestNumber));
                }
            }
        }
        private bool _guestRated = false;
        public bool GuestRated
        {
            get => _guestRated;
            set
            {
                if (value != _guestRated)
                {
                    _guestRated = value;
                    OnPropertyChanged(nameof(GuestRated));
                }
            }
        }
        private bool _ownerRated = false;
        public bool OwnerRated
        {
            get => _ownerRated;
            set
            {
                if (value != _ownerRated)
                {
                    _ownerRated = value;
                    OnPropertyChanged(nameof(OwnerRated));
                }
            }
        }
        public bool Canceled = false;

        public AccommodationReservation() { }

        public AccommodationReservation(int accommodationId, int guestId, DateTime startDate, DateTime endDate, int numberOfDays, int guestsNumber)
        {
            Accommodation.Id = accommodationId;
            Guest.Id = guestId;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfDays;
            Canceled = false;
            GuestNumber = guestsNumber;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                Guest.Id.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                NumberOfDays.ToString(),
                GuestNumber.ToString(),
                GuestRated.ToString(),
                OwnerRated.ToString(),
                Canceled.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Accommodation.Id = int.Parse(values[1]);
            Guest.Id = int.Parse(values[2]);
            StartDate = DateTime.Parse(values[3]);
            EndDate = DateTime.Parse(values[4]);
            NumberOfDays = int.Parse(values[5]);
            GuestNumber = int.Parse(values[6]);
            GuestRated = bool.Parse(values[7]);
            OwnerRated = bool.Parse(values[8]);
            Canceled = bool.Parse(values[9]);
        }
    }
}
