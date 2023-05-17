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
    public class RenovationSuggestionRepo : IRenovationSuggestionRepo
    {
        private readonly RenovationSuggestionFileHandler _filehandler;
        private List<RenovationSuggestion> _renovations;

        public RenovationSuggestionRepo()
        {
            _filehandler = new();
            _renovations = new();
            Load();
        }

        public List<RenovationSuggestion> GetAll()
        {
            return _renovations;
        }

        public RenovationSuggestion GetById(int renovationId)
        {
            return _renovations.Find(x => x.Id == renovationId);
        }

        public void Load()
        {
            _renovations = _filehandler.Load();
        }

        public int NextId()
        {
            return _renovations.Count > 0 ? _renovations.Max(x => x.Id) + 1 : 1;
        }

        public RenovationSuggestion Save(RenovationSuggestion renovation)
        {
            renovation.Id = NextId();
            _renovations.Add(renovation);
            _filehandler.Save(_renovations);
            return renovation;
        }

        public void SaveAll(List<RenovationSuggestion> renovations)
        {
            _filehandler.Save(renovations);
            _renovations = renovations;
        }

    }
}
