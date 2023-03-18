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

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmation.xaml
    /// </summary>
    public partial class AccommodationReservationConfirmation : Window
    {
        public Accommodation Accommodation { get; set; } = new();
        public AccommodationReservationConfirmation(Accommodation accommodation)
        {
            InitializeComponent();
            Accommodation = accommodation;
            NameTextBlock.Text = Accommodation.Name;
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Button(object sender, RoutedEventArgs e)
        {
            var confirm = new FreeAccommodationsSuggestions();
            confirm.Show();
        }
    }
}
