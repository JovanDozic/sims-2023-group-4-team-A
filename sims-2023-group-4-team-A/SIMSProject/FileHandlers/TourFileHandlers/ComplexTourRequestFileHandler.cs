using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandlers.TourFileHandlers
{
    public class ComplexTourRequestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/complextourrequests.csv";
        private readonly Serializer<ComplexTourRequest> _serializer;

        public ComplexTourRequestFileHandler()
        {
            _serializer = new Serializer<ComplexTourRequest>();
        }

        public List<ComplexTourRequest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<ComplexTourRequest> complexTourRequests)
        {
            _serializer.ToCSV(FilePath, complexTourRequests);
        }
    }
}
