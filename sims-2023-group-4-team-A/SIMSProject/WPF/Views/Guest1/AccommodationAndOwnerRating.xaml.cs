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
using SIMSProject.Model;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.Models.AccommodationModels;


using SIMSProject.Controller;
using System.Collections.ObjectModel;
using SIMSProject.Controller.UserController;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;


namespace SIMSProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerRating.xaml
    /// </summary>
    public partial class AccommodationAndOwnerRating : Window
    {
        private readonly User User = new();
        private readonly OwnerRatingViewModel _ownerRatingViewModel;
        private AccommodationReservation _accommodationReservation;
        public AccommodationAndOwnerRating(User user)
        {
            InitializeComponent();
            User = user;
            _ownerRatingViewModel = new(User, _accommodationReservation);
            DataContext = _ownerRatingViewModel;
            _ownerRatingViewModel.AddReservationsToCombo();
           
        }
        private void ReservationsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_ownerRatingViewModel.IsSelected())
            {
                OwnerNameTextBlock.Text = _ownerRatingViewModel.GetOwnerUsername(); 
            }
            
        }

    }
}
