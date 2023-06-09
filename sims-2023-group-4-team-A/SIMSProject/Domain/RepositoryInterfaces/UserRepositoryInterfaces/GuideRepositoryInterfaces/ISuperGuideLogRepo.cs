using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces.GuideRepositoryInterfaces
{
    public interface ISuperGuideLogRepo
    {
        public SuperGuideLog Get(int id, Language language);
        public List<SuperGuideLog> GetAll();
        public SuperGuideLog Save(SuperGuideLog log);
        public void SaveAll(List<SuperGuideLog> logs);
        public void Delete(int id, Language language);
        public bool CheckIfSuper(Guide guide, Language language);
    }
}
