using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface ICommentRepo
    {
        public void Load();
        public Comment GetById(int commentId);
        public List<Comment> GetAll();
        public int NextId();
        public Comment Save(Comment comment);
        public void SaveAll(List<Comment> comments);
    }
}
