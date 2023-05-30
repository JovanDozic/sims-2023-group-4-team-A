using SIMSProject.Domain.Models.AccommodationModels;
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
        private List<Forum> _forums;

        public ForumRepo()
        {
            _forums = new List<Forum>();
            _fileHandler = new ForumFileHandler();
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
    }
}
