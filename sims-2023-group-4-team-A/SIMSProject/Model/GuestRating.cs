using SIMSProject.Serializer;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.Model
{
    public class GuestRating : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        private int _cleanliness;
        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (_cleanliness != value && value >= 1 && value <= 5)
                {
                    _cleanliness = value;
                    OnPropertyChanged(nameof(Cleanliness));
                }
            }
        }
        private int _rulesAcceptance;
        public int RulesAcceptance
        {
            get => _rulesAcceptance;
            set
            {
                if (_rulesAcceptance != value && value >= 1 && value <= 5) 
                {
                    _rulesAcceptance = value;
                    OnPropertyChanged(nameof(RulesAcceptance));
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
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        public GuestRating() { }

        public GuestRating(int id, int cleanliness, int rulesAcceptance, string comment)
        {
            Id = id;
            Cleanliness = cleanliness;
            RulesAcceptance = rulesAcceptance;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Cleanliness.ToString(),
                RulesAcceptance.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Cleanliness = int.Parse(values[1]);
            RulesAcceptance = int.Parse(values[2]);
            Comment = values[3];
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
