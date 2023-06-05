using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
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
            _repo.Save(comment);
        }

        public List<Comment> GetAllByUser(User user)
        {
            return _repo.GetAll().Where(x => x.User.Id == user.Id).ToList();
        }
    }
}
