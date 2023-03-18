using System;
using SIMSProject.Serializer;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.Model
{
    public class AccommodationReservation: ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public int GuestId { get; set; }
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
        public int GuestsNumber
        {
            get => _guestsNumber;
            set
            {
                if(value != _guestsNumber)
                {
                    _guestsNumber = value;
                    OnPropertyChanged(nameof(GuestsNumber));
                }
            }
        }

        public bool Canceled = false;

        public AccommodationReservation() { }

        public AccommodationReservation(int accommodationId, int guestId, DateTime startDate, DateTime endDate, int numberOfDays, int guestsNumber)
        {
            AccommodationId = accommodationId;
            GuestId = guestId;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfDays;
            Canceled = false;
            GuestsNumber = guestsNumber;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                GuestId.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                NumberOfDays.ToString(),
                GuestsNumber.ToString(),
                Canceled.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            GuestId = int.Parse(values[2]);
            StartDate = DateTime.Parse(values[3]);
            EndDate = DateTime.Parse(values[4]);
            NumberOfDays = int.Parse(values[5]);
            GuestsNumber = int.Parse(values[6]);
            Canceled = bool.Parse(values[7]);
        }
    }
}
