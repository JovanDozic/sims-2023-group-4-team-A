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
    /// Interaction logic for AccommodationSearchAndShowForm.xaml
    /// </summary>
    public partial class AccommodationSearchAndShowForm : Window, IObserver
    {
        public Accommodation Accommodation { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        private AccommodationController AccommodationControllers;
        public AccommodationSearchAndShowForm()
        {
            InitializeComponent();

            DataContext = this;

            Accommodation = new();


            AccommodationControllers = new AccommodationController();


            Accommodations = new ObservableCollection<Accommodation>(AccommodationControllers.GetAll());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Accommodations.Clear();

            foreach (var accommodations in new ObservableCollection<Accommodation>(AccommodationControllers.GetAll()))
            {
                Accommodations.Add(accommodations);
            }

            String search1 = TextSearch1.Text;
            if (search1 == "Naziv tip lokacija") search1 = string.Empty;
            List<Accommodation> searchResults = new();
            List<string> searchList = new List<string>();

            string[] searchValues = search1.Split(" ");

            int searchNumberOfGuests = TextSearch2.Value <= 0 ? -1 : TextSearch2.Value;

            int searchNumberOfDays = TextSearch3.Value <= 0 ? -1 : TextSearch3.Value;

            



            foreach (string value in searchValues)
            {
                foreach (var acc in Accommodations)
                {
                    if (acc.ToString().ToLower().Contains(value.ToLower()))
                    {


                        if (searchNumberOfGuests == -1)
                        {
                            if (searchNumberOfDays == -1)
                                searchResults.Add(acc);
                            else if (acc.MinReservationDays <= searchNumberOfDays)
                                searchResults.Add(acc);
                        }
                        else if (acc.MaxGuestNumber >= searchNumberOfGuests)
                        {
                            if (searchNumberOfDays == -1)
                                searchResults.Add(acc);
                            else if (acc.MinReservationDays <= searchNumberOfDays)
                                searchResults.Add(acc);
                        }
                        
                        



                    }

                }
            }


            Accommodations.Clear();

            foreach (var searchResult in searchResults)
            {
                Accommodations.Add(searchResult);
            }


        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        private void TextSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            textbox.Foreground = new SolidColorBrush(Colors.Black);
            if (textbox.Text == "Naziv tip lokacija") textbox.Text = string.Empty;
        }

        private void TextSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textbox = sender as TextBox;
            if (textbox is null) return;
            if (textbox.Text == string.Empty)
            {
                textbox.Foreground = new SolidColorBrush(Colors.Gray);
                textbox.Text = "Naziv tip lokacija";

            }
        }
    }
}
