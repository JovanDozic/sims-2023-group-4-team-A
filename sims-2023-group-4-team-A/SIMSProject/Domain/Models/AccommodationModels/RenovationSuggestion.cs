using System;
using System.Collections.Generic;
using System.Linq;
using SIMSProject.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class RenovationSuggestion: ISerializable
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string LevelOfEmergency { get; set; } = string.Empty;
        public RenovationSuggestion()
        {

        }
        public RenovationSuggestion(string comment, string levelOfEmergency)
        {
            Comment = comment;
            LevelOfEmergency = levelOfEmergency;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Comment,
                LevelOfEmergency,
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Comment = values[1];
            LevelOfEmergency = values[2];
        }
    }
}
