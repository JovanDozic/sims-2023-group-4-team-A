﻿using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class TourAppointmentFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourAppointments.csv";
        private readonly Serializer<TourAppointment> _serializer;

        public TourAppointmentFileHandler()
        {
            _serializer = new Serializer<TourAppointment>();
        }

        public List<TourAppointment> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourAppointment> tourAppointments)
        {
            _serializer.ToCSV(FilePath, tourAppointments);
        }
    }
}
