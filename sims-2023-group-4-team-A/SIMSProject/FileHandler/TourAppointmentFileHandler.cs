using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Model.DAO;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class TourAppointmentFileHandler: CSVManager<TourAppointment>
    {
        private const string FilePath = "../../../Resources/Data/tourAppointments.csv";

        public TourAppointmentFileHandler(): base(FilePath)
        {
        }

        public List<TourAppointment> Load()
        {
            return FromCSV();
        }

        public void Save(List<TourAppointment> tourAppointments)
        {
            ToCSV(tourAppointments);
        }

        protected override TourAppointment ParseItemFromCSV(string[] values)
        {
            TourAppointment tourAppointment = new()
            {
                Id = Convert.ToInt32(values[0]),
                Date = DateTime.Parse(values[1]),
                TourId = Convert.ToInt32(values[2]),
                TourStatus = (Status)Enum.Parse(typeof(Status), values[3]),
                AvailableSpots = Convert.ToInt32(values[4]),
                CurrentKeyPointId = Convert.ToInt32(values[5])
            };
            return tourAppointment;
        }

        protected override string[] ParseItemToCsv(TourAppointment appointment)
        {
            string[] csvValues = { appointment.Id.ToString(),
                appointment.Date.ToString("dd.MM.yyyy HH:mm:ss"),
                appointment.TourId.ToString(),
                appointment.TourStatus.ToString(),
                appointment.AvailableSpots.ToString(),
                appointment.CurrentKeyPointId.ToString() };
            return csvValues;
        }
    }
}
