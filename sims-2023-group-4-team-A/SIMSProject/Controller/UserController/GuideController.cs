using System.Collections.Generic;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model.DAO.UserModelDAO;

namespace SIMSProject.Controller.UserController
{
    public class GuideController
    {
        private readonly GuideDAO _guides;
        public Guide Guide;

        public GuideController()
        {
            _guides = new GuideDAO();
            Guide = new Guide();
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