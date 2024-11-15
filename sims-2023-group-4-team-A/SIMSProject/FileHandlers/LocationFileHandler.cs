﻿using System.Collections.Generic;
using SIMSProject.Domain.Models;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers
{
    public class LocationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";
        private readonly Serializer<Location> serializer;

        public LocationFileHandler()
        {
            serializer = new Serializer<Location>();
        }

        public List<Location> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<Location> tourlocations)
        {
            serializer.ToCSV(FilePath, tourlocations);
        }
    }
}