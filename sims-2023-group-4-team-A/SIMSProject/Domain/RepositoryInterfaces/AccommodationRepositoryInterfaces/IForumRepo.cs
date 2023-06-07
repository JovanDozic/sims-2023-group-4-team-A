using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IForumRepo
    {
        public void Load();
        public void CheckAndUpdateUsability();
        public Forum GetById(int forumId);
        public List<Forum> GetAll();
        public int NextId();
        public Forum Save(Forum forum);
        public void SaveAll(List<Forum> forums);
        void Update(Forum forum);
    }
}
