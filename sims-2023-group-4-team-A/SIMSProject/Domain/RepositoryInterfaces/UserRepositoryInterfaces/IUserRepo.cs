﻿using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces
{
    public interface IUserRepo
    {
        public User GetByIdAndRole(int id, UserRole role);

        public Owner? GetOwnerById(int id);
        public List<Owner> GetAllOwners();

        public Guide? GetGuideById(int id);
        public List<Guide> GetAllGuides();

        public Guest? GetGuestById(int id);
        public List<Guest> GetAllGuests();

    }
}
