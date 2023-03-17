using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model.UserModel;

namespace SIMSProject.Model
{
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourDate()
        {
            
        }

        public override string ToString()
        {
            return $"{Date}";
        }

        public TourDate(int id, DateTime dateTime, int tourId)
        {
            Id = id;
            Date = dateTime;
            TourId = tourId;
        }

            public void FromCSV(string[] values)
            {
                Id = Convert.ToInt32(values[0]);
                Date = DateTime.Parse(values[1]);
                TourId = Convert.ToInt32(values[2]);
            }

            public string[] ToCSV()
            {
                string[] csvValues = { Id.ToString(), Date.ToString(), TourId.ToString() };
                return csvValues;
            }
    }
}
