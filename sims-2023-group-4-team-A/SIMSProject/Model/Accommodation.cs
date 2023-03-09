﻿using SIMSProject.Controller;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model
{
    public enum ACCOMMODATION_TYPE { APARTMENT = 0, HOUSE, HUT};
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Address Location { get; set; }
        public ACCOMMODATION_TYPE Type { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int CancellationThreshold { get; set; } = 1;
        public List<string> ImageURLs { get; set; }

        public Accommodation()
        {
            Location = new();
            ImageURLs = new();
        }

        public Accommodation(string name, Address location, ACCOMMODATION_TYPE type, int maxGuestNumber, int minReservationDays, int cancellationThreshold, List<string> imageURLs)
        {
            Name = name;
            Location = location;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            CancellationThreshold = cancellationThreshold;
            ImageURLs = imageURLs;
        }

        private ACCOMMODATION_TYPE GetType(string type)
        {
            if (type == "APARTMENT") return ACCOMMODATION_TYPE.APARTMENT;
            else if (type == "HOUSE") return ACCOMMODATION_TYPE.HOUSE;
            else return ACCOMMODATION_TYPE.HUT;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Location.Id.ToString(), Type.ToString(), MaxGuestNumber.ToString(), MinReservationDays.ToString(), CancellationThreshold.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            AddressController addressController = new();
            Location = addressController.GetById(int.Parse(values[3]));
            Type = GetType(values[4]);
            MaxGuestNumber = int.Parse(values[5]);
            MinReservationDays = int.Parse(values[6]);
            CancellationThreshold = int.Parse(values[7]);
            // TODO: ImageURLs problem
        }
    }
}
