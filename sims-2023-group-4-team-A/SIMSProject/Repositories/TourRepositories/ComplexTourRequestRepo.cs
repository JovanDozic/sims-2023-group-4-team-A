using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandlers.TourFileHandlers;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.TourRepositories
{
    public class ComplexTourRequestRepo : IComplexTourRequestRepo
    {
        public readonly ComplexTourRequestFileHandler _fileHandler;
        private readonly IGuest2Repo _guestRepo;
        private List<ComplexTourRequest> _complexTourRequests;
        public ComplexTourRequestRepo(IGuest2Repo guestRepo)
        {
            _fileHandler = new ComplexTourRequestFileHandler();
            _complexTourRequests = _fileHandler.Load();
            _guestRepo = guestRepo;
            MapGuests();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequests;
        }

        public List<ComplexTourRequest> GetAllByGuestId(int guestId)
        {
            return _complexTourRequests.FindAll(x => x.Guest.Id == guestId);
        }

        public int NextId()
        {
            return _complexTourRequests.Count > 0 ? _complexTourRequests.Max(x => x.Id) + 1 : 1;
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            complexTourRequest.Id = NextId();
            _complexTourRequests.Add(complexTourRequest);
            _fileHandler.Save(_complexTourRequests);
            return complexTourRequest;
        }

        public void SaveAll(List<ComplexTourRequest> complexTourRequests)
        {
            _fileHandler.Save(complexTourRequests);
            _complexTourRequests = complexTourRequests;
        }

        private void MapGuests()
        {
            foreach (var complexTourRequest in _complexTourRequests)
            {
                complexTourRequest.Guest = _guestRepo.GetById(complexTourRequest.Guest.Id);
            }
        }
    }
}
