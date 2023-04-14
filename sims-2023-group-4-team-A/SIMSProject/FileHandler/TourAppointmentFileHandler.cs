using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Model.DAO;
using SIMSProject.Serializer;
using System.Collections.Generic;


namespace SIMSProject.FileHandler
{
    public class TourAppointmentFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourAppointments.csv";
        private readonly Serializer<TourAppointment> _serializer;

        public TourAppointmentFileHandler()
        {
            _serializer = new Serializer<TourAppointment>();
        }

        public List<TourAppointment> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourAppointment> tourAppointments)
        {
            _serializer.ToCSV(FilePath, tourAppointments);
        }
    }
}
