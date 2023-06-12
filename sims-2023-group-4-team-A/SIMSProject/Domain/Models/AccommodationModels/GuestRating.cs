using System.Globalization;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class GuestRating : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; } = new();
        public int CleanlinessRating { get; set; } = 1;
        public int ComplianceWithRules { get; set; } = 1;
        public int PaymentAndBilling { get; set; } = 1;
        public int CommunicationRating { get; set; } = 1;
        public int Recommendation { get; set; } = 1;
        public double Overall { get => CalculateOverall(); set { } }
        public string Comment { get; set; } = string.Empty;

        public GuestRating()
        {
        }

        private double CalculateOverall()
        {
            return (CleanlinessRating + ComplianceWithRules + PaymentAndBilling + CommunicationRating + Recommendation) / (double)5;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                CleanlinessRating.ToString(),
                ComplianceWithRules.ToString(),
                PaymentAndBilling.ToString(),
                CommunicationRating.ToString(),
                Recommendation.ToString(),
                Overall.ToString(CultureInfo.CurrentCulture),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Reservation.Id = int.Parse(values[1]);
            CleanlinessRating = int.Parse(values[2]);
            ComplianceWithRules = int.Parse(values[3]);
            PaymentAndBilling = int.Parse(values[4]);
            CommunicationRating = int.Parse(values[5]);
            Recommendation = int.Parse(values[6]);
            Overall = double.Parse(values[7]);
            Comment = values[8];
        }

        public override string ToString()
        {
            return $"Id: {Id}\nResId: {Reservation.Id}\ncln:{CleanlinessRating},ruls:{ComplianceWithRules},pay:{PaymentAndBilling},cmctn:{CommunicationRating},rcmnd:{Recommendation},/nOverall: {Overall}\nComment: {Comment}";
        }
    }
}