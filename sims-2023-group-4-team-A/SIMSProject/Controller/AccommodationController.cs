﻿using SIMSProject.Model.DAO;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Controller
{
    public class AccommodationController
    {
        private AccommodationDAO _accommodations;
        public Accommodation Accommodation;

        public AccommodationController()
        {
            _accommodations = new();
            Accommodation = new();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations.GetAll();
        }

        public void SaveAll(List<Accommodation> accommodations)
        {
            _accommodations.SaveAll(accommodations);
        }

        public Accommodation Create(Accommodation accommodation)
        {
            return _accommodations.Save(accommodation);
        }
    }
}
