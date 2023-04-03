using System.ComponentModel;
using System.Runtime.CompilerServices;
using SIMSProject.Serializer;

namespace SIMSProject.Model
{
    public class GuestRating : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; } = new();
        private int _cleanliness = 1;
        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (_cleanliness != value && value >= 1 && value <= 5)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _rulesAcceptance = 1;
        public int RulesAcceptance
        {
            get => _rulesAcceptance;
            set
            {
                if (_rulesAcceptance != value && value >= 1 && value <= 5)
                {
                    _rulesAcceptance = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _comment = string.Empty;
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _overall;
        public double Overall
        {
            get
            {
                _overall = (Cleanliness + RulesAcceptance) / (double)2;
                OnPropertyChanged();
                return _overall;
            }
            set
            {
                _overall = value;
                OnPropertyChanged();
            }
        }

        public GuestRating()
        {
        }

        public GuestRating(int id, int reservationId, int cleanliness, int rulesAcceptance, string comment)
        {
            Id = id;
            Reservation.Id = reservationId;
            Cleanliness = cleanliness;
            RulesAcceptance = rulesAcceptance;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                Cleanliness.ToString(),
                RulesAcceptance.ToString(),
                Overall.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Reservation.Id = int.Parse(values[1]);
            Cleanliness = int.Parse(values[2]);
            RulesAcceptance = int.Parse(values[3]);
            Overall = double.Parse(values[4]);
            Comment = values[5];
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}