using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class CommentService
    {
        private readonly ICommentRepo _repo;
        private AccommodationReservationService _reservationService;

        public CommentService(ICommentRepo repo)
        {
            _repo = repo;
            _reservationService = Injector.GetService<AccommodationReservationService>();
        }

        public List<Comment> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Comment> GetAllByUser(User user)
        {
            return _repo.GetAll().Where(x => x.User.Id == user.Id).ToList();
        }

        public Comment CreateComment(Comment newComment, Location forumLocation)
        {
            if (newComment.User.Role == UserRole.Guest1 || newComment.User.Role == UserRole.SuperGuest)
            {
                var reservations = _reservationService.GetAllUncancelledByUser(newComment.User);
                if (reservations.Any(x => x.Accommodation.Location.Id == forumLocation.Id)) newComment.WasAtLocation = true;
                else newComment.WasAtLocation = false;
            }
            else newComment.WasAtLocation = true;

            newComment.CreationDate = DateTime.Now;

            _repo.Save(newComment);
            return newComment;
        }

        public void UpdateComment(Comment hoveredComment)
        {
            _repo.Update(hoveredComment);
        }
    }
}
