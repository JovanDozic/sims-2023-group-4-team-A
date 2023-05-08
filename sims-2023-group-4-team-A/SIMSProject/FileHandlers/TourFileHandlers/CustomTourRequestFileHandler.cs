using System.Collections.Generic;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers.TourFileHandlers
{
    public class CustomTourRequestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/customtourrequests.csv";
        private readonly Serializer<CustomTourRequest> _serializer;

        public CustomTourRequestFileHandler()
        {
            _serializer = new Serializer<CustomTourRequest>();
        }
        
        public List<CustomTourRequest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<CustomTourRequest> tourRequests)
        {
            _serializer.ToCSV(FilePath, tourRequests);
        }

    }
}
