using SIMSProject.Controller;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SIMSProject.View
{
    /// <summary>
    /// Interaction logic for TourCreationWindow.xaml
    /// </summary>
    ///

    public enum TranslatedLanguage { Engleski = 0, Sprski, Spanski, Francuski};
   
    public partial class TourCreationWindow : Window , INotifyPropertyChanged
    {

        private static int keyPointCounter = 0;
        public TourController tourController { get; set; } = new();
        public KeyPointController keyPointController { get; set; } = new();
        public TourDateController tourDateController { get; set; } = new();
        public TourLocationController tourLocationController { get; set; } = new();


        


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Tour New { get; set; }
        public TourLocation NewAddress { get; set; }
        public TranslatedLanguage NewTranslate { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; } = new();
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public string SelectedTime { get; set; } = string.Empty;
        public string Images { get; set;} = string.Empty;
        public User Guide {  get; set; }

        public List<KeyPoint> NewKeyPoints { get; set; } = new();
        public List<TourDate> NewDates { get; set; } = new();
        public List<KeyPoint> KeyPoints { get; set; } = new();

        public TourCreationWindow()
        {
            InitializeComponent();
            this.DataContext = this;


            AddTranslatedLanguages();

            New = new Tour();
            NewAddress = new TourLocation();


            Guide = new User("Admin", "Admin");
            Guide.Id = 256;

            KeyPoints = keyPointController.GetAll();

        }

        private void AddTranslatedLanguages()
        {
            foreach(var enumValue in  Enum.GetValues(typeof(TranslatedLanguage))) 
            {
                LanguageCombo.Items.Add(enumValue);
            }
        }

        private Language FindCorespondive(TranslatedLanguage translate)
        {
            foreach(Language value in Enum.GetValues(typeof(Language)))
            { 
                if((int)value == (int)translate)
                {
                    return value;
                }
            }
            throw new NotImplementedException("Invalid enum types! Please check this.");
        }


             

        private List<string> SeperateURLs()
        {
           List<string> seperatedURLS = new List<string>();
           string[] urls =  Images.Split(",");
            seperatedURLS.AddRange(urls);
            return seperatedURLS;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(keyPointCounter >= 2)
            {
                

                
                New.KeyPoints = NewKeyPoints;
                New.Dates = NewDates;
                New.TourLanguage = FindCorespondive(NewTranslate);
                New.Guide = Guide;
                New.GuideId = Guide.Id;
                //New.LocationId = tourLocationController.GetAll().Find(x => x.Country == New.Location.Country && x.City == New.Location.City).Id;

                List<string> images = SeperateURLs();
                New.Images = images;

                keyPointCounter = 0;

                tourController.Create(New);
                MessageBox.Show("Tura uspešno kreirana.");
                Close();
                
            }
            else
            {
                MessageBox.Show("Morate uneti najmanje 2 ključne tačke!");
            }            

        }

        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            NewKeyPoints.Add(SelectedKeyPoint);
            keyPointCounter++;

        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            string[] timeParts = SelectedTime.Split(":");
            int hours = int.Parse(timeParts[0]);
            int minutes = int.Parse(timeParts[1]);
            int seconds = 0;

            DateTime newDate = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, hours, minutes, seconds);
            NewDates.Add(new(-1, SelectedDate, -1));
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
