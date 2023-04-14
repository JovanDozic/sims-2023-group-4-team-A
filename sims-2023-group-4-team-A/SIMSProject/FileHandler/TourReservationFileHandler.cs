﻿using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandler
{
    public class TourReservationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourreservation.csv";
        private readonly Serializer<TourReservation> serializer;

        public TourReservationFileHandler()
        {
            serializer = new Serializer<TourReservation>();
        }

        public List<TourReservation> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<TourReservation> tourReservation)
        {
            serializer.ToCSV(FilePath, tourReservation);
        }
    }
}
