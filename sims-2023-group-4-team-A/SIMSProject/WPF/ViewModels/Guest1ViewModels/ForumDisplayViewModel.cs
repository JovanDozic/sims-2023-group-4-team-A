using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.Guest1ViewModels
{
    public class ForumDisplayViewModel : ViewModelBase
    {
        private readonly User _user = new();
        private Forum _forum = new();
        private ForumService _service;
        public Forum Forum
        {
            get { return _forum; }
            set { _forum = value; }
        }

        public ForumDisplayViewModel(User user, Forum forum)
        {
            _service = Injector.GetService<ForumService>();
            Forum = forum;
            _user = user;
        }

        public void CloseForum()
        {
            _service.CloseForum(Forum);
        }
    }
}
