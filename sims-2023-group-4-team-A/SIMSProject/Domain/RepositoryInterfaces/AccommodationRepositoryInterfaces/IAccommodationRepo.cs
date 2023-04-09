﻿using SIMSProject.Model;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IAccommodationRepo
    {
        public Accommodation GetById(int accommodationId);
        public List<Accommodation> GetByOwnerId(int ownerId);
        public List<Accommodation> GetAll();
        public int NextId();
        public Accommodation Save(Accommodation accommodation);
        public void SaveAll(List<Accommodation> accommodations);
    }
}
