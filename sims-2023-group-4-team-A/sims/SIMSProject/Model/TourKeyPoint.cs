using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model.UserModel;

namespace SIMSProject.Model
{
    public class TourKeyPoint : ISerializable
    {
        public int TourId { get; set; }
        public int KeyPointId { get; set; }

        public TourKeyPoint()
        {

        }

        public TourKeyPoint(int tourId, int keyPointId)
        {
            TourId = tourId;
            KeyPointId = keyPointId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { TourId.ToString(), KeyPointId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            TourId = Convert.ToInt32(values[0]);
            KeyPointId = Convert.ToInt32(values[1]);
        }
    }
}
