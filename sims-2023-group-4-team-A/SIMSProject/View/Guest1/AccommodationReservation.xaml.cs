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
    /// Interaction logic for AccommodationReservation.xaml
    /// </summary>
    public partial class AccommodationReservation : Window
    {
        public Accommodation Accommodation { get; set; }
        public AccommodationReservation(Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = this;
            Accommodation = accommodation;
            TextBoxNaziv.Text = accommodation.Name;
            TextBoxLokacija.Text = accommodation.Location.ToString();
            TextBoxTip.Text = accommodation.Type;


        }
    }
}
