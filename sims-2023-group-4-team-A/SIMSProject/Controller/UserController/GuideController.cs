using SIMSProject.Model.DAO.UserModelDAO;
using SIMSProject.Model.UserModel;
using System.Collections.Generic;

namespace SIMSProject.Controller.UserController
{
    public class GuideController
    {
        private GuideDAO _guides;
        public Guide Guide;

        public GuideController()
        {
            _guides = new();
            Guide = new();
        }

        public List<Guide> GetAll()
        {
            return _guides.GetAll();
        }

        public void SaveAll(List<Guide> guide)
        {
            _guides.SaveAll(guide);
        }

        public Guide Create(Guide guide)
        {
            return _guides.Save(guide);
        }

        public Guide GetByID(int id)
        {
            return _guides.Get(id);
        }
    }
}
