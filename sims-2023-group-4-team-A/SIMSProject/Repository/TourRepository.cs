﻿using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";
        private readonly Serializer<Tour> serializer;

        public TourRepository()
        {
            serializer = new Serializer<Tour>();
        }

        public List<Tour> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<Tour> tours)
        {
            serializer.ToCSV(FilePath, tours);
        }
    }
}
