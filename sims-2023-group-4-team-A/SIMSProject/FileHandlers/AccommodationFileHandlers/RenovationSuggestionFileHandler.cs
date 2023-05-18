using System.Collections.Generic;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers.AccommodationFileHandlers
{
    public class RenovationSuggestionFileHandler
    {
        private const string FilePath = "../../../Resources/Data/renovationSuggestions.csv";
        private readonly Serializer<RenovationSuggestion> _serializer;

        public RenovationSuggestionFileHandler()
        {
            _serializer = new Serializer<RenovationSuggestion>();
        }

        public List<RenovationSuggestion> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<RenovationSuggestion> renovations)
        {
            _serializer.ToCSV(FilePath, renovations);
        }
    }
}
