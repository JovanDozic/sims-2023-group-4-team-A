using SIMSProject.Controller;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMSProject.Model
{
    public class GuestRating : ISerializable, INotifyPropertyChanged
    {
        // TODO: trebalo bi da primarni kljuc bude po rezervaciji, ali mi nemamo klasu rezervacije

        public int Id { get; set; }
        private int _cleanliness;
        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (_cleanliness != value)
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
                if (_rulesAcceptance != value)
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
