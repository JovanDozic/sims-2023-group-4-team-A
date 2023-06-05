using SIMSProject.Domain.Models.AccommodationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IForumRepo
    {
        public void Load();
        public Forum GetById(int forumId);
        public List<Forum> GetAll();
        public int NextId();
        public Forum Save(Forum forum);
        public void SaveAll(List<Forum> forums);
    }
}
