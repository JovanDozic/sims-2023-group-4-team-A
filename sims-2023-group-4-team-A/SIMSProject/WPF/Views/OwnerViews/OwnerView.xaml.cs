using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Repositories;
using SIMSProject.Repositories.AccommodationRepositories;
using SIMSProject.Repositories.UserRepositories;
using SIMSProject.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SIMSProject.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class OwnerView : Window, INotifyPropertyChanged
    {

        private ObservableCollection<Accommodation> _accs = new();

        public ObservableCollection<Accommodation> Accs
        {
            get => _accs;
            set
            {
                if (value == _accs) return;
                _accs = value;
                OnPropertyChanged();
            }
        }

        public readonly AccommodationService AccService;


        public OwnerView()
        {
            InitializeComponent();
            DataContext = this;
            AccService = new(new AccommodationRepo(new LocationRepo(), new OwnerRepo()));
            Accs = new(AccService.GetAllByOwnerId(1));

            foreach (var ac in AccService.GetAllByOwnerId(1))
            {
                Accs.Add(ac);
                Accs.Add(ac);
                Accs.Add(ac);
                Accs.Add(ac);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
