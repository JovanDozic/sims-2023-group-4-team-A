using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repository
{
    public class KeyPointRepository
    {
        private const string FilePath = "../../../Resources/Data/keypoints.csv";
        private readonly Serializer<KeyPoint> serializer;

        public KeyPointRepository()
        {
            serializer = new Serializer<KeyPoint>();
        }

        public List<KeyPoint> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<KeyPoint> keyPoints) 
        { 
            serializer.ToCSV(FilePath, keyPoints);
        }
    }
}
