using SIMSProject.Domain.Models;
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

        public SuperGuideLog GetByGuideId(int id)
        {
            return _logs.FirstOrDefault(x => x.GuideId == id);
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
    }
}
