using System.Collections.Generic;

namespace SIMSProject.Domain.Models
{
    public class ImageSerializer
    {
        public string ImageURLsToCSV(List<string> images)
        {
            string imagesCSV = string.Empty;
            if (images.Count > 0)
            {
                imagesCSV = string.Empty;
                foreach (var imageURL in images)
                {
                    imagesCSV += imageURL + ",";
                }
                imagesCSV = imagesCSV.Remove(imagesCSV.Length - 1);
            }
            return imagesCSV;
        }

        public List<string> ImageURLsFromCSV(string value)
        {
            List<string> imageURLs = new();
            foreach (var imageURL in value.Split(','))
            {
                if (imageURL != string.Empty)
                {
                    imageURLs.Add(imageURL);
                }
            }
            return imageURLs;
        }
    }
}
