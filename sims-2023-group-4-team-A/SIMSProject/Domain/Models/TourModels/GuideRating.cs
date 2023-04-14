using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Xps.Serialization;

namespace SIMSProject.Domain.Models.TourModels
{
    public  class GuideRating : ISerializable
    {
        public int Id { get; set; }
        public int TourReservationId { get; set; }
        public TourReservation TourReservation { get; set; } = new();
        public int GuideKnowledge { get; set; } = 1;
        public int LanguageProficiency { get; set; } = 1;
        public int TourEntertainmentRating { get; set; } = 1;
        public int OrganizationQualityRating { get; set; } = 1;
        public double Overall
        {
            get
            {
                return (GuideKnowledge + LanguageProficiency + TourEntertainmentRating + OrganizationQualityRating) / (double)4;
            }
            set { }
        }
        public string Comment { get; set; } = string.Empty;

        public GuideRating() { }

        public GuideRating(int id, int tourReservationId, TourReservation tourReservation, int guideKnowledge, int languageProficiency, int tourEntertainmentRating, int organizationQualityRating, double overall, string comment)
        {
            Id = id;
            TourReservationId = tourReservationId;
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
            string[] csvValues =
            {
                Id.ToString(),
                TourReservationId.ToString(),
                GuideKnowledge.ToString(),
                LanguageProficiency.ToString(),
                TourEntertainmentRating.ToString(),
                OrganizationQualityRating.ToString(),
                Overall.ToString(CultureInfo.CurrentCulture),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourReservationId = int.Parse(values[1]);
            GuideKnowledge = int.Parse(values[2]);
            LanguageProficiency = int.Parse(values[3]);
            TourEntertainmentRating = int.Parse(values[4]);
            OrganizationQualityRating = int.Parse(values[5]);
            Overall = int.Parse(values[6]);
            Comment = values[7];
        }
    }
}
