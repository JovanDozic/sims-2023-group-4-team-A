using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.Models
{
    public class SuperGuideLog: ISerializable
    {
        public int GuideId { get; set; }
        public Language Language { get; set; }
        public DateTime Obtained { get; set; }
        public bool Expired { get => DateTime.Compare(DateTime.Now, Obtained.AddYears(1)) > 0; }
        public SuperGuideLog()
        {
        }

        public SuperGuideLog(int guideId, Language language, DateTime obtained) 
        {
            GuideId = guideId;
            Language = language;
            Obtained = obtained;
        }

        public string[] ToCSV()
        {
            string[] values = {GuideId.ToString(), Language.ToString(), Obtained.ToString() };
            return values;
        }

        public void FromCSV(string[] values)
        {
            GuideId = int.Parse(values[0]);
            Language = (Language)Enum.Parse(typeof(Language), values[1]);
            Obtained = DateTime.Parse(values[2]);
        }
    }
}
