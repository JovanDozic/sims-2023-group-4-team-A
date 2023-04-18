using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using SIMSProject.Domain.Models.UserModels;

namespace SIMSProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for ReservationReqeusts.xaml
    /// </summary>
    public partial class ReservationReqeusts : Window
    {
        private readonly ReschedulingRequestViewModel _reschedulingRequestViewModel;
        private readonly User _user;
        private AccommodationReservation _accommodationReservation { get; set; }
        public ReservationReqeusts(User _user)
        {
            InitializeComponent();
            _user = new User();
            _reschedulingRequestViewModel = new(_user, _accommodationReservation);
            DataContext = _reschedulingRequestViewModel;
            _reschedulingRequestViewModel.AddRequestsToCombo();
        }

        private void RequestsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_reschedulingRequestViewModel.IsSelected())
            {
                //RequestStatusTB.Text = _reschedulingRequestViewModel.GetRequestStatus().Status.ToString();
                CommentTB.Text = _reschedulingRequestViewModel.SelectedRequest.OwnerComment;
            }
        }
    }
}
