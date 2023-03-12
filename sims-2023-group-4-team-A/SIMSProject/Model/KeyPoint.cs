using SIMSProject.Controller;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Repository;

namespace SIMSProject.Model
{
    public class KeyPoint : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        private int _locationId;
        public int LocationId
        {
            get => _locationId;
            set
            {
                if (value != _locationId)
                {
                    _locationId = value;
                    OnPropertyChanged(nameof(LocationId));
                }
            }
        }

        public TourLocation Location { get; set; }

        public List<Tour> Tours { get; set; } =  new List<Tour>();
        public KeyPoint()
        {
            
        }
        public KeyPoint(int id, string description, TourLocation location, int locationId)
        {
            Id = id;
            Description = description;
            Location = location;
            LocationId = locationId;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return $"{Description}";
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Description, LocationId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Description = values[1];
            LocationId = Convert.ToInt32(values[2]);         
        }
    }
}
