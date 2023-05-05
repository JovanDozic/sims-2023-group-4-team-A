using SIMSProject.Serializer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerRegisterAccommodationView : Page, INotifyPropertyChanged
    {

        private List<string> _images = new();
        public List<string> ImageSources
        {
            get => _images;
            set
            {
                if (value == _images) return;
                _images = value;
                OnPropertyChanged();
            }
        }

        public OwnerRegisterAccommodationView()
        {
            InitializeComponent();


            ImageSources.Add("https://i.ibb.co/tp345Vc/img1.jpg");
            ImageSources.Add("https://i.ibb.co/nPxQb8T/img2.jpg");
            ImageSources.Add("https://i.ibb.co/F33nHYg/img3.jpg");
            DataContext = this;



        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
