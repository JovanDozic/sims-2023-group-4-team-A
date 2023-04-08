using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Model.UserModel;
using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SIMSProject.FileHandler
{
    public class TourFileHandler : CSVManager<Tour>
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        public TourFileHandler() : base(FilePath)
        {
        }

        public List<Tour> Load()
        {
            return FromCSV();
        }

        public void Save(List<Tour> tours)
        {
            ToCSV(tours);
        }

        protected override Tour ParseItemFromCSV(string[] values)
        {
            Tour tour = new();
            tour.Id = Convert.ToInt32(values[0]);
            tour.Name = values[1];
            tour.Description = values[2];
            tour.TourLanguage = (Language)Enum.Parse(typeof(Language), values[3]);
            tour.MaxGuestNumber = Convert.ToInt32(values[4]);
            tour.Duration = Convert.ToInt32(values[5]);
            tour.LocationId = Convert.ToInt32(values[6]);
            tour.GuideId = Convert.ToInt32(values[7]);
            tour.Images.AddRange(values[8].Split(','));
            return tour;
        }

        protected override string[] ParseItemToCsv(Tour tour)
        {
            string[] csvValues = {
                tour.Id.ToString(),
                tour.Name,
                tour.Description,
                tour.TourLanguage.ToString(),
                tour.MaxGuestNumber.ToString(),
                tour.Duration.ToString(),
                tour.LocationId.ToString(),
                tour.GuideId.ToString(),
                tour.CreateImageURLs().ToString() };
            return csvValues;
        }
    }
}
