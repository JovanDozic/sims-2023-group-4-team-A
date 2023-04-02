using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SIMSProject.Serializer;

namespace SIMSProject.Model
{
    enum RESCHEDULING_REQUEST_STATUS
    {
        WAITING = 0,
        ACCEPTED,
        DECLINED
    }

    public class ReschedulingRequest : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        private AccommodationReservation _accommodationReservation = new();
        public AccommodationReservation AccommodationReservation
        {
            get => _accommodationReservation;
            set
            {
                if (value == _accommodationReservation) return;
                _accommodationReservation = value;
                OnPropertyChanged();
            }
        }
        private DateTime _newStartDate;
        public DateTime NewStartDate
        {
            get => _newStartDate;
            set
            {
                if (_newStartDate == value) return;
                _newStartDate = value;
                OnPropertyChanged();
            }
        }
        private string _ownerComment = string.Empty;
        public string OwnerComment
        {
            get => _ownerComment;
            set
            {
                if (_ownerComment == value) return;
                _ownerComment = value;
                OnPropertyChanged();
            }
        }
        private RESCHEDULING_REQUEST_STATUS _status;
        public string Status
        {
            get =>
                _status switch
                {
                    RESCHEDULING_REQUEST_STATUS.WAITING => "Na čekanju",
                    RESCHEDULING_REQUEST_STATUS.ACCEPTED => "Odobren",
                    _ => "Odbijen"
                };
            set
            {
                _status = value switch
                {
                    "Na čekanju" => RESCHEDULING_REQUEST_STATUS.WAITING,
                    "Odobren" => RESCHEDULING_REQUEST_STATUS.ACCEPTED,
                    _ => RESCHEDULING_REQUEST_STATUS.DECLINED
                };
                OnPropertyChanged();
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservation.Id.ToString(),
                NewStartDate.ToString(),
                OwnerComment,
                Status
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            var i = 0;
            Id = int.Parse(values[i++]);
            AccommodationReservation.Id = int.Parse(values[i++]);
            NewStartDate = DateTime.Parse(values[i++]);
            OwnerComment = values[i++];
            Status = values[i++];
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
