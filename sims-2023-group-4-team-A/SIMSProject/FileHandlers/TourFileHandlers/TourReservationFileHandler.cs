﻿using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandlers.TourFileHandlers
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
