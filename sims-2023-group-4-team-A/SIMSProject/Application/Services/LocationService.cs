﻿using SIMSProject.Domain.Models;
using SIMSProject.Domain.RepositoryInterfaces;

namespace SIMSProject.Application.Services
{
    public class LocationService
    {
        private readonly ILocationRepo _repo;

        public LocationService(ILocationRepo repo)
        {
            _repo = repo;
        }

        public Location GetLocation(string city, string country)
        {
            var location = _repo.GetByInfo(city, country);
            if (location is not null) return location;

            location = new Location(city, country);
            _repo.Save(location);
            return location;
        }

    }
}