using System.Collections.Generic;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers.AccommodationFileHandlers
{
    public class AccommodationReservationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/accommodationReservations.csv";
        private readonly Serializer<AccommodationReservation> _serializer;

        public AccommodationReservationFileHandler()
        {
            _serializer = new Serializer<AccommodationReservation>();
        }

        public List<AccommodationReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationReservation> reservations)
        {
            _serializer.ToCSV(FilePath, reservations);
        }
    }
}