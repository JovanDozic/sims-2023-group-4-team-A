using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.FileHandlers.AccommodationFileHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    public class ForumRepo : IForumRepo
    {
        private readonly ForumFileHandler _fileHandler;
        private ILocationRepo _locationRepo;
        private ICommentRepo _commentRepo;
        private List<Forum> _forums;

        public ForumRepo(ILocationRepo locationRepo, ICommentRepo commentRepo)
        {
            _forums = new List<Forum>();
            _fileHandler = new ForumFileHandler();
            _locationRepo = locationRepo;
            _commentRepo = commentRepo;

            Load();
        }

        public List<Forum> GetAll()
        {
            return _forums;
        }

        public Forum GetById(int forumId)
        {
            return _forums.Find(x => x.Id == forumId);
        }

        public void Load()
        {
            _forums = _fileHandler.Load();

            MapLocations();
            MapComments();
        }

        private void MapComments()
        {
            foreach (var forum in _forums)
            {
                List<Comment> updatedComments = new();
                foreach (var comment in forum.Comments)
                {
                    var updatedComment = _commentRepo.GetById(comment.Id);
                    updatedComments.Add(updatedComment);
                }
                forum.Comments = updatedComments;
            }
        }

        private void MapLocations()
        {
            foreach(var forum in _forums)
            {
                forum.Location = _locationRepo.GetById(forum.Location.Id);
            }
        }

        public int NextId()
        {
            return _forums.Count > 0 ? _forums.Max(x => x.Id) + 1 : 1;
        }

        public Forum Save(Forum forum)
        {
            forum.Id = NextId();
            _forums.Add(forum);
            _fileHandler.Save(_forums);
            return forum;
        }

        public void SaveAll(List<Forum> forums)
        {
            _fileHandler.Save(forums);
            _forums = forums;
        }

        public void Update(Forum forum)
        {
            var forumToUpdate = _forums.Find(x => x.Id == forum.Id);
            var index = _forums.IndexOf(forumToUpdate);
            _forums[index] = forum;
            _fileHandler.Save(_forums);
        }
    }
}
