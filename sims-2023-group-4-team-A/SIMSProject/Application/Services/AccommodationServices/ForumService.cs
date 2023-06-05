﻿using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class ForumService
    {
        private readonly IForumRepo _repo;

        public ForumService(IForumRepo repo)
        {
            _repo = repo;
        }

        public List<Forum> GetAll()
        {
            return _repo.GetAll();
        }

        public void CreateForum(Forum forum)
        {
            _repo.Save(forum);
        }

        public List<Forum> GetAllByUser(User user)
        {
            return _repo.GetAll().Where(f => f.Comments.First().Id == user.Id).ToList();
        }
    }
}
