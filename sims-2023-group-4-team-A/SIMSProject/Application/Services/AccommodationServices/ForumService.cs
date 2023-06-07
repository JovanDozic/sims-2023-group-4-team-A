﻿using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class ForumService
    {
        private readonly IForumRepo _repo;
        private CommentService _commentService;

        public ForumService(IForumRepo repo)
        {
            _repo = repo;
            _commentService = Injector.GetService<CommentService>();
        }

        public List<Forum> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Forum> GetAllByUser(User user)
        {
            return _repo.GetAll().Where(f => f.Comments.First().Id == user.Id).ToList();
        }

        public List<Location> GetAllLocations()
        {
            var locations = GetAll().Select(f => f.Location).DistinctBy(x => x.Id).ToList();
            foreach (var location in locations)
            {
                location.ForumsCount = GetAll().Where(f => f.Location.Id == location.Id).Count();
            }
            return locations;
        }

        public List<Forum> GetAllByLocation(Location location)
        {
            return GetAll().FindAll(x => x.Location.Id == location.Id);
        }

        public void CreateForum(Location location, Comment firstComment)
        {
            firstComment = _commentService.CreateComment(firstComment, location);
            Forum forum = new()
            {
                Location = location,
                CreationDate = DateTime.Now,
            };
            forum.Comments.Add(firstComment);
            _repo.Save(forum);
        }

        public Comment AddNewComment(Forum forum, Comment newComment)
        {
            newComment = _commentService.CreateComment(newComment, forum.Location);
            forum.Comments.Add(newComment);
            _repo.Update(forum);
            return newComment;
        }

        public Comment DownvoteComment(Comment hoveredComment)
        {
            if (hoveredComment.UserDownvoted)
            {
                hoveredComment.UserDownvoted = false;
                hoveredComment.Downvotes--;
                _commentService.UpdateComment(hoveredComment);
                return hoveredComment;
            }
            hoveredComment.UserDownvoted = true;
            hoveredComment.Downvotes++;
            _commentService.UpdateComment(hoveredComment);
            return hoveredComment;
        }

        public void CheckAndUpdateUsability()
        {
            _repo.CheckAndUpdateUsability();
        }

        public void CloseForum(Forum forum)
        {
            forum.IsClosed = true;
            _repo.Update(forum);
        }
    }
}
