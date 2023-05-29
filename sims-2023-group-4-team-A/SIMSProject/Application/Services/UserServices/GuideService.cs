using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces.GuideRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.UserServices
{
    public class GuideService
    {
        private readonly ISuperGuideLogRepo _superGuideLogRepo;
        private readonly IGuideRepo _repo;
        private readonly GuideRatingService _ratingService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourService _tourService;

        public GuideService(IGuideRepo guideRepo,ISuperGuideLogRepo repo)
        {
            _repo = guideRepo;
            _superGuideLogRepo = repo;
            _ratingService = Injector.GetService<GuideRatingService>();
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourService = Injector.GetService<TourService>();
        }

        public string DisplaySuperGuideLanguages(Guide guide)
        {
            var languages = NoteLanguages(guide);
            if (languages.Equals("")) return "";
            _tourService.SortBySuperGuide(guide.Id);
            StringBuilder st = new("Supervodič za jezik/jezike: ");
            st.Append(languages);
            return st.ToString();
        }

        private string NoteLanguages(Guide guide)
        {
            StringBuilder st = new(" ");
            foreach (Language language in Enum.GetValues(typeof(Language)))
            {
                var log = _superGuideLogRepo.Get(guide.Id, language);
                if (log != null)
                {
                    if (!log.Expired)
                    {
                        st.Append($"{Tour.GetLanguage(language)},");
                        continue;
                    }
                    _superGuideLogRepo.Delete(guide.Id, language);
                }
                if (_ratingService.IsSuperGuide(_tourAppointmentService.GetSuperGuideEligible(guide.Id, language)))
                {
                    CreateLog(guide.Id, language);
                    _tourService.FlagSuperGuide(guide.Id, language);
                    st.Append($"{Tour.GetLanguage(language)},");
                }
            }
            st.Remove(st.Length - 1, 1);
            return st.ToString();
        }
        private void CreateLog(int guideId, Language language)
        {
            var log = new SuperGuideLog(guideId, language, DateTime.Now);
            _superGuideLogRepo.Save(log);
        }

        public void Quit(int guideId)
        {
            _repo.Quit(guideId);
        }
    }
}
