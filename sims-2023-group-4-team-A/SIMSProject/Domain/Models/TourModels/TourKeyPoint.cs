using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model.UserModel;

namespace SIMSProject.Domain.Models.TourModels
{
    public class TourKeyPoint
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
    }
}
