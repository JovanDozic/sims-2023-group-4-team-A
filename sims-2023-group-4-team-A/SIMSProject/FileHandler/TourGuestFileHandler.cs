using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class TourGuestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourguests.csv";
        private readonly Serializer<TourGuest> _serializer;
        public  TourGuestFileHandler()
        {
            _serializer = new Serializer<TourGuest>();
        }

        public List<TourGuest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourGuest> tourGuests)
        {
            _serializer.ToCSV(FilePath, tourGuests);
        }
    }
}
