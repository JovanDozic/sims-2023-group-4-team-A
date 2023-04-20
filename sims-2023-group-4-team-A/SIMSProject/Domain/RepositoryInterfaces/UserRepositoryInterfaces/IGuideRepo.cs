using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces
{
    public interface IGuideRepo
    {
        public Guide GetById(int guideId);
        public List<Guide> GetAll();
        public int NextId();
        public Guide Save(Guide guide);
        public void SaveAll(List<Guide> guides);
        public void Update(Guide guide);
    }
}
