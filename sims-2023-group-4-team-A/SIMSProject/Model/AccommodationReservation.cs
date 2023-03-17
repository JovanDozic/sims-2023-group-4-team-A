using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Controller;
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

        public bool Canceled = false;

        public AccommodationReservation()
        {

        }

        public AccommodationReservation(int id, int accommodationId, int guestId, DateTime startDate, DateTime endDate, int numberOfDays, bool canceled)
        {
            Id = id;
            AccommodationId = accommodationId;
            GuestId = guestId;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfDays;
            Canceled = canceled;
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
            Canceled = bool.Parse(values[6]);
        }

        
    }

    
}
