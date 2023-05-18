﻿using SIMSProject.Serializer;
using System;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class AccommodationRenovation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; } = new();
        public DateTime StartDate { get; set;  } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public int NumberOfDays { get; set; } = 1;
        public string Description { get; set; } = string.Empty;
        public bool IsCancelled { get; set; } = false;

        public AccommodationRenovation() { }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                NumberOfDays.ToString(),    
                Description,
                IsCancelled.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            Accommodation.Id = int.Parse(values[i++]);
            StartDate = DateTime.Parse(values[i++]);
            EndDate = DateTime.Parse(values[i++]);
            NumberOfDays = int.Parse(values[i++]);
            Description = values[i++];
            IsCancelled = bool.Parse(values[i++]);
        }

    }
}