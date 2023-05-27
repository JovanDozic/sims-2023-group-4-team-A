using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.UserServices
{
    public class GuideService
    {
        private readonly IGuideRepo _repo;
        private readonly GuideRatingService _ratingService;
        private readonly TourAppointmentService _tourAppointmentService;

        public GuideService(IGuideRepo repo)
        {
            _repo = repo;
            _ratingService = Injector.GetService<GuideRatingService>();
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            
        }

        public string CheckIfSuperGuide(Guide guide)
        {
            if (!NoteLanguages(guide).Equals(""))
            {
                StringBuilder st = new("Supervodič za jezik/jezike: ");
                st.Append(NoteLanguages(guide));
                BecomeSuperGuide(guide);
                return st.ToString();
            }
            BecomeRegular(guide);
            return "";
        }

        private string NoteLanguages(Guide guide)
        {
            StringBuilder st = new(" ");
            foreach(Language language in Enum.GetValues(typeof(Language)))
            {
                if (_ratingService.IsSuperGuide(_tourAppointmentService.GetSuperGuideEligible(guide.Id, language)))
                {
                    st.Append($"{Tour.GetLanguage(language)},");
                }
            }
            st.Remove(st.Length - 1, 1);
            return st.ToString();
        }
        private void BecomeSuperGuide(Guide guide)
        {
            guide.Role = Domain.Models.UserRole.SuperGuide;
            _repo.Update(guide);
        }
        private void BecomeRegular(Guide guide)
        {
            guide.Role = Domain.Models.UserRole.Guide;
            _repo.Update(guide);
        }
    }
}
