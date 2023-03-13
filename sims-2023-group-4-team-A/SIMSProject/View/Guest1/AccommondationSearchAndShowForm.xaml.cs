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
using SIMSProject.Observer;
using SIMSProject.Controller;
using System.Collections.ObjectModel;
using SIMSProject.View.Guest1;


namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommondationSearchAndShowForm.xaml
    /// </summary>
    public partial class AccommondationSearchAndShowForm : Window,  IObserver
    {
        public Accommodation Accommodation { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get;}
        private readonly AccommodationController AccommodationControllers;
        public AccommondationSearchAndShowForm()
        {
            InitializeComponent();

            DataContext = this;

            Accommodation = new();
            

            AccommodationControllers = new AccommodationController();


            Accommodations = new ObservableCollection<Accommodation>(AccommodationControllers.GetAll());
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String search = TextSearch.Text;


        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
