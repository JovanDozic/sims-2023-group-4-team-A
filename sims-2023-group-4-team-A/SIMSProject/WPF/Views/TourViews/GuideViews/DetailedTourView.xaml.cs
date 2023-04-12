﻿using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModel.TourViewModels;
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

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for DetailedTourView.xaml
    /// </summary>
    public partial class DetailedTourView : Window
    {
        private TourAppointmentsViewModel _appointments;
        public DetailedTourView(Tour tour)
        {
            InitializeComponent();
            _appointments = new(tour);
            DataContext = _appointments;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _appointments.CancelAppointment();
        }
    }
}
