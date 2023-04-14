﻿using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;


namespace SIMSProject.FileHandler
{
    public class TourAppointmentFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourAppointments.csv";
        private readonly Serializer<TourAppointment> serializer;

        public TourAppointmentFileHandler()
        {
            serializer = new Serializer<TourAppointment>();
        }

        public List<TourAppointment> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<TourAppointment> tourAppointments)
        {
            serializer.ToCSV(FilePath, tourAppointments);
        }
    }
}
