using SIMSProject.Domain.RepositoryInterfaces;

namespace SIMSProject.Application.Services
{
    public class LocationService
    {
        private readonly ILocationRepo _repo;

        public LocationService(ILocationRepo repo)
        {
            _repo = repo;
        }

    }
}
