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

        public TourController tourController { get; set; } = new();
        public KeyPointController KeyPointController { get; set; } = new();

        public Tour New { get; set; }

        public KeyPoint SelectedKeyPoint { get; set; } = new();
        
        public List<KeyPoint> NewKeyPoints { get; set; } = new();
        public List<KeyPoint> KeyPoints { get; set; } = new();


        public TranslatedLanguage NewTranslate { get; set; }

        public TourLocation NewAddress { get; set; }
        public DateTime TourDate { get; set; } = DateTime.Now;

        public User Guide {  get; set; }

        public TourCreationWindow()
        {
            InitializeComponent();
            this.DataContext = this;


            AddEnums();

            New = new Tour();
            NewAddress = new TourLocation();
            Guide = new User("Admin", "Admin");
            Guide.Id = 256;

            KeyPoints = KeyPointController.GetAll();

        }

        public void AddEnums()
        {
            foreach(var enumValue in  Enum.GetValues(typeof(TranslatedLanguage))) 
            {
                LanguageCombo.Items.Add(enumValue);
            }
        }

        public  Language FindCorespondive(TranslatedLanguage translate)
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



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

            TourDate tourDate = new TourDate(-1,TourDate ,-1);

            New.KeyPoints = NewKeyPoints;
            New.Location = NewAddress;
            New.TourLanguage = FindCorespondive(NewTranslate);
            New.Dates.Add(tourDate);
            New.Guide = Guide;
            New.GuideId = Guide.Id;
            New.LocationId = NewAddress.Id;
            tourController.Create(New);

        }

        private void AddKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            NewKeyPoints.Add(SelectedKeyPoint);

        }
    }
}
