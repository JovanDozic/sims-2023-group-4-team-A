using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandlers.UserFileHandler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.UserRepositories
{
    public class GuideRepo : IGuideRepo
    {
        private GuideFileHandler _fileHandler;
        private List<Guide> _guides;

        public GuideRepo()
        {
            _fileHandler = new GuideFileHandler();
            _guides = _fileHandler.Load();
        }

        public List<Guide> GetAll()
        {
            return _guides;
        }

        public Guide GetById(int guideId)
        {
            return _guides.Find(x => x.Id == guideId);
        }

        public int NextId()
        {
            return _guides.Count > 0 ? _guides.Max(x => x.Id) + 1 : 1;
        }

        public Guide Save(Guide guide)
        {
            guide.Id = NextId();
            _guides.Add(guide);
            _fileHandler.Save(_guides);
            return guide;
        }

        public void SaveAll(List<Guide> guides)
        {
            _fileHandler.Save(guides);
            _guides = guides;
        }

        public void Update(Guide guide)
        {
            Guide guideToUpdate = GetById(guide.Id) ?? throw new Exception("Updating guide failed!");
            int index = _guides.IndexOf(guideToUpdate);
            _guides[index] = guide;
            _fileHandler.Save(_guides);
        }
    }
}
