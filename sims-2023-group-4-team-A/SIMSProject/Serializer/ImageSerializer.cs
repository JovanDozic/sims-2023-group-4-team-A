using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Serializer
{
    public class ImageSerializer
    {
        public static string ImageURLsToCSV(List<string> images)
        {
            return images.Count > 0 ? string.Join(",", images) : string.Empty;
        }

        public static List<string> ImageURLsFromCSV(string value)
        {
            return value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}