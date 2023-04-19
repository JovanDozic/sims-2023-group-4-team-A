using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandlers.AccommodationFileHandlers
{
    public class ReschedulingRequestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/reschedulingrequests.csv";
        private readonly Serializer<ReschedulingRequest> _serializer;

        public ReschedulingRequestFileHandler()
        {
            _serializer = new();
        }

        public List<ReschedulingRequest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<ReschedulingRequest> requests)
        {
            _serializer.ToCSV(FilePath, requests);
        }
    }
}
