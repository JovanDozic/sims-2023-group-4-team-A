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

        public CommentService(ICommentRepo repo)
        {
            _repo = repo;
        }

        public List<Comment> GetAll()
        {
            return _repo.GetAll();
        }

        public void CreateForum(Comment comment)
        {
            // TODO: treba da se zabelezi da li j egost bio na toj lokaciji
            _repo.Save(comment);
        }

        public List<Comment> GetAllByUser(User user)
        {
            return _repo.GetAll().Where(x => x.User.Id == user.Id).ToList();
        }

        public Comment CreateComment(Comment newComment)
        {
            newComment.CreationDate = DateTime.Now;
            _repo.Save(newComment);
            return newComment;
        }
    }
}
