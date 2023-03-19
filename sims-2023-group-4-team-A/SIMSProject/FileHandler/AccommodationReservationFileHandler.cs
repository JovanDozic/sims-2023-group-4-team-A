using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandler
{
    public class AccommodationReservationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/accommodationReservation.csv";

        private readonly Serializer<AccommodationReservation> _serializer;

        public AccommodationReservationFileHandler()
        {
            _serializer = new();
        }

        public List<AccommodationReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationReservation> accommodationReservation)
        {
            _serializer.ToCSV(FilePath, accommodationReservation);
        }


    }
}
