using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;


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
                if(value != _currentKeyPointId)
                {
                    _currentKeyPointId = value;
                    OnPropertyChanged(nameof(CurrentKeyPointId));
                }
            }
        }

        public Tour Tour { get; set; } = new();
        public KeyPoint CurrentKeyPoint { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourDate(){}

        public TourDate(int id, DateTime date, int tourId, int currentKeyPointId)
        {
            Id = id;
            Date = date;
            TourId = tourId;
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
                CurrentKeyPointId = Convert.ToInt32(values[4]);
            }

            public string[] ToCSV()
            {
                string[] csvValues = { Id.ToString(), Date.ToString(), TourId.ToString(), TourStatus, CurrentKeyPointId.ToString() };
                return csvValues;
            }
    }
}
