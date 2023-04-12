using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Model.UserModel;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class TourGuestFileHandler: CSVManager<TourGuest>
    {
        private const string FilePath = "../../../Resources/Data/tourguests.csv";

        public  TourGuestFileHandler(): base(FilePath)
        {
        }

        public List<TourGuest> Load()
        {
            return FromCSV();
        }

        public void Save(List<TourGuest> tourGuests)
        {
            ToCSV(tourGuests);
        }

        protected override TourGuest ParseItemFromCSV(string[] values)
        {
            TourGuest tourGuest = new()
            {
                AppointmentId = Convert.ToInt32(values[0]),
                GuestId = Convert.ToInt32(values[1]),
                GuestStatus = (GuestAttendance)Enum.Parse(typeof(GuestAttendance), values[2]),
                JoinedKeyPointId = Convert.ToInt32(values[3])
            };
            return tourGuest;
        }

        protected override string[] ParseItemToCsv(TourGuest tourGuest)
        {
            string[] csvValues = { tourGuest.AppointmentId.ToString(), tourGuest.GuestId.ToString(), tourGuest.GuestStatus.ToString(), tourGuest.JoinedKeyPointId.ToString() };
            return csvValues;
        }
    }
}
