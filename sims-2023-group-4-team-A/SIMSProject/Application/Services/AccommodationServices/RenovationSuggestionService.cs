using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class RenovationSuggestionService
    {
        private readonly IRenovationSuggestionRepo _repo;
        public RenovationSuggestionService(IRenovationSuggestionRepo repo)
        {
            _repo = repo;
        }

        public List<RenovationSuggestion> GetAll()
        {
            return _repo.GetAll();
        }

        public void SendRequest(RenovationSuggestion renovation)
        {
            _repo.Save(renovation);
        }
    }
}
