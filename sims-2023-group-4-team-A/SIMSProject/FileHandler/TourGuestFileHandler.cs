using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandler
{
    public class TourGuestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourguests.csv";
        private readonly Serializer<TourGuest> serializer;

        public  TourGuestFileHandler()
        {
            serializer = new Serializer<TourGuest>();
        }

        public List<TourGuest> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<TourGuest> tourGuests)
        {
            serializer.ToCSV(FilePath, tourGuests);
        }
    }
}
