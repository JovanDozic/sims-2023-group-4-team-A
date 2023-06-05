using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandlers.AccommodationFileHandlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    internal class CommentRepo : ICommentRepo
    {
        private readonly CommentFileHandler _fileHandler;
        private IUserRepo _userRepo;
        private List<Comment> _comments;

        public CommentRepo(IUserRepo userRepo)
        {
            _comments = new();
            _fileHandler = new();
            _userRepo = userRepo;

            Load();
        }

        public List<Comment> GetAll()
        {
            return _comments;
        }

        public Comment GetById(int commentId)
        {
            return _comments.Find(x => x.Id == commentId);
        }

        public void Load()
        {
            _comments = _fileHandler.Load();

            MapUsers();
        }

        private void MapUsers()
        {
            foreach (var comment in _comments)
            {
                comment.User = _userRepo.GetByIdAndRole(comment.User.Id, comment.User.Role);
            }
        }

        public int NextId()
        {
            return _comments.Count > 0 ? _comments.Max(x => x.Id) + 1 : 1;
        }

        public Comment Save(Comment comment)
        {
            comment.Id = NextId();
            _comments.Add(comment);
            _fileHandler.Save(_comments);
            return comment;
        }

        public void SaveAll(List<Comment> comments)
        {
            _fileHandler.Save(comments);
            _comments = comments;
        }
    }
}
