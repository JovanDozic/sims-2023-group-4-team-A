using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces.GuideRepositoryInterfaces;
using SIMSProject.FileHandlers.UserFileHandler.GuideFileHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.UserRepositories.GuideRepositories
{
    public class SuperGuideLogRepo : ISuperGuideLogRepo
    {
        private SuperGuideLogFileHandler _fileHandler;
        private List<SuperGuideLog> _logs;

        public SuperGuideLogRepo()
        {
            _fileHandler = new SuperGuideLogFileHandler();
            _logs = _fileHandler.Load();
        }
        public List<SuperGuideLog> GetAll()
        {
            return _logs;
        }

        public SuperGuideLog Get(int id, Language language)
        {
            return _logs.FirstOrDefault(x => x.GuideId == id && x.Language == language);
        }

        public SuperGuideLog Save(SuperGuideLog log)
        {
            _logs.Add(log);
            _fileHandler.Save(_logs);
            return log;
        }

        public void SaveAll(List<SuperGuideLog> logs)
        {
            _fileHandler.Save(logs);
            _logs = logs;
        }

        public void Delete(int id, Language language)
        {
            var log = _logs.Find(x => x.GuideId == id &&  (x.Language == language));
            _logs.Remove(log);
            _fileHandler.Save(_logs);
        }
    }
}
