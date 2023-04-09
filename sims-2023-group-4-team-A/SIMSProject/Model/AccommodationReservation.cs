using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;

namespace SIMSProject.Model
{
    public class AccommodationReservation : ISerializable, INotifyPropertyChanged
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
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _numberOfDays;
        public int NumberOfDays
        {
            get => _numberOfDays;
            set
            {
                if (value != _numberOfDays)
                {
                    _numberOfDays = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _guestsNumber;
        public int GuestNumber
        {
            get => _guestsNumber;
            set
            {
                if (value != _guestsNumber)
                {
                    _guestsNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _guestRated;
        public bool GuestRated
        {
            get => _guestRated;
            set
            {
                if (value != _guestRated)
                {
                    _guestRated = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _ownerRated;
        public bool OwnerRated
        {
            get => _ownerRated;
            set
            {
                if (value != _ownerRated)
                {
                    _ownerRated = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Canceled;

        public AccommodationReservation()
        {
        }

        public AccommodationReservation(int accommodationId, int guestId, DateTime startDate, DateTime endDate,
            int numberOfDays, int guestsNumber, bool canceled)
        {
            Accommodation.Id = accommodationId;
            Guest.Id = guestId;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfDays;
            Canceled = canceled;
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