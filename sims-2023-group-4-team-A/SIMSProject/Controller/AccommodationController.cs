using System;
using System.Collections.Generic;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Model;
using SIMSProject.Model.DAO;

namespace SIMSProject.Controller
{
    public class AccommodationController
    {
        private readonly AccommodationDAO _accommodations;
        public Accommodation Accommodation;

        public AccommodationController()
        {
            _accommodations = new AccommodationDAO();
            Accommodation = new Accommodation();
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

        public AccommodationType GetType(string type)
        {
            if (type == "Apartman")
            {
                return AccommodationType.Apartment;
            }

            if (type == "Kuća")
            {
                return AccommodationType.House;
            }

            return AccommodationType.Hut;
        }

        public List<Accommodation> GetAllByOwner(int id)
        {
            return _accommodations.GetAll().FindAll(x => x.Owner.Id == id);
        }
    }
}