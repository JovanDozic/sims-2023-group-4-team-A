using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SIMSProject.Domain.Models.TourModels
{
    public  class GuideRating : ImageSerializer, ISerializable
    {
        public int Id { get; set; }
        public TourReservation TourReservation { get; set; } = new();
        public int GuideKnowledge { get; set; } = 1;
        public int LanguageProficiency { get; set; } = 1;
        public int TourEntertainmentRating { get; set; } = 1;
        public int OrganizationQualityRating { get; set; } = 1;
        public DateTime RatingDate { get; set; } = DateTime.Now;
        public bool Reported { get; set; } = false;
        public double Overall
        {
            get
            {
                return Math.Round((GuideKnowledge + LanguageProficiency + TourEntertainmentRating + OrganizationQualityRating) / (double)4, 2);
            }
            set { }
        }
        public string Comment { get; set; } = string.Empty;
        public List<string> ImageURLs { get; set; } = new();
        public string ImageURLsCSV { get; set; } = string.Empty;

        public GuideRating() { }

        public GuideRating(int id, int tourReservationId, TourReservation tourReservation, int guideKnowledge, int languageProficiency, int tourEntertainmentRating, int organizationQualityRating, double overall, string comment)
        {
            Id = id;
            TourReservation.Id = tourReservationId;
            TourReservation = tourReservation;
            GuideKnowledge = guideKnowledge;
            LanguageProficiency = languageProficiency;
            TourEntertainmentRating = tourEntertainmentRating;
            OrganizationQualityRating = organizationQualityRating;
            Overall = overall;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            ImageURLsCSV = ImageURLsToCSV(ImageURLs);
            string[] csvValues =
            {
                Id.ToString(),
                TourReservation.Id.ToString(),
                GuideKnowledge.ToString(),
                LanguageProficiency.ToString(),
                TourEntertainmentRating.ToString(),
                OrganizationQualityRating.ToString(),
                Overall.ToString(CultureInfo.CurrentCulture),
                Comment,
                RatingDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                Reported.ToString(),
                ImageURLsCSV
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourReservation.Id = int.Parse(values[1]);
            GuideKnowledge = int.Parse(values[2]);
            LanguageProficiency = int.Parse(values[3]);
            TourEntertainmentRating = int.Parse(values[4]);
            OrganizationQualityRating = int.Parse(values[5]);
            Overall = double.Parse(values[6]);
            Comment = values[7];
            RatingDate = DateTime.Parse(values[8], CultureInfo.GetCultureInfo("sr-LATN"));
            Reported = bool.Parse(values[9]);
            ImageURLsCSV = values[10];
            ImageURLs = ImageURLsFromCSV(ImageURLsCSV);
        }


        public string RatingDateToString()
        {
            return RatingDate.ToString("g");
        }

        public string QAsToString()
        {
            var builder = new StringBuilder();
            builder.Append($"Znanje vodiča:     {GuideKnowledge}\n");
            builder.Append($"Poznavanje jezika: {LanguageProficiency}\n");
            builder.Append($"Zanimljivost ture: {TourEntertainmentRating}\n");
            builder.Append($"Organizacija:      {OrganizationQualityRating}\n");
            return builder.ToString();
        }
    }
}
