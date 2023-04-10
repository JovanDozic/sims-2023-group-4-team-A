using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;

namespace SIMSProject.Model
{
    public class GuestRating : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; } = new();

        private int _cleanlinessRating = 1;
        public int CleanlinessRating
        {
            get => _cleanlinessRating;
            set
            {
                if (_cleanlinessRating == value || value is < 1 or > 5) return;
                _cleanlinessRating = value;
                OnPropertyChanged();
            }
        }

        private int _complianceWithRules = 1;
        public int ComplianceWithRules
        {
            get => _complianceWithRules;
            set
            {
                if (_complianceWithRules == value || value is < 1 or > 5) return;

                _complianceWithRules = value;
                OnPropertyChanged();
            }
        }

        private int _paymentAndBilling = 1;
        public int PaymentAndBilling
        {
            get => _paymentAndBilling;
            set
            {
                if (value == _paymentAndBilling || value is < 1 or > 5) return;
                _paymentAndBilling = value;
                OnPropertyChanged();
            }
        }

        private int _communicationRating = 1;
        public int CommunicationRating
        {
            get => _communicationRating;
            set
            {
                if (value == _communicationRating || value is < 1 or > 5) return;
                _communicationRating = value;
                OnPropertyChanged();
            }
        }

        private int _recommendation = 1;
        public int Recommendation
        {
            get => _recommendation;
            set
            {
                if (value == _recommendation || value is < 1 or > 5) return;
                _recommendation = value;
                OnPropertyChanged();
            }
        }

        private double _overall;
        public double Overall
        {
            get
            {
                _overall = (CleanlinessRating + ComplianceWithRules + PaymentAndBilling + CommunicationRating + Recommendation) / (double)5;
                OnPropertyChanged();
                return _overall;
            }
            set
            {
                if (Math.Abs(_overall - value) < 0) return;
                _overall = value;
                OnPropertyChanged();
            }
        }

        private string _comment = string.Empty;
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment == value) return;

                _comment = value;
                OnPropertyChanged();
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                CleanlinessRating.ToString(),
                ComplianceWithRules.ToString(),
                PaymentAndBilling.ToString(),
                CommunicationRating.ToString(),
                Recommendation.ToString(),
                Overall.ToString(CultureInfo.CurrentCulture),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Reservation.Id = int.Parse(values[1]);
            CleanlinessRating = int.Parse(values[2]);
            ComplianceWithRules = int.Parse(values[3]);
            PaymentAndBilling = int.Parse(values[4]);
            CommunicationRating = int.Parse(values[5]);
            Recommendation = int.Parse(values[6]);
            Overall = double.Parse(values[7]);
            Comment = values[8];
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}