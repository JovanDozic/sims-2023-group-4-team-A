using SIMSProject.Domain.Models.AccommodationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IRenovationSuggestionRepo
    {
        public void Load();
        public RenovationSuggestion GetById(int renovationId);
        public List<RenovationSuggestion> GetAll();
        public int NextId();
        public RenovationSuggestion Save(RenovationSuggestion renovation);
        public void SaveAll(List<RenovationSuggestion> renovations);
    }
}
