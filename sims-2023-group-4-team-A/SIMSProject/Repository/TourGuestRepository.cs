using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repository
{
    public class TourGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourguests.csv";
        private readonly Serializer<TourGuest> serializer;

        public  TourGuestRepository()
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
