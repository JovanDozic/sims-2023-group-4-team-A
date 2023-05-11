﻿using Dynamitey;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels
{
    public class AppointmentPickerViewModel : ViewModelBase
    {
        private readonly TourAppointmentService _tourAppointmentService;
        private Tour _tour { get; set; }
        private TourAppointment _active { get => _tourAppointmentService.GetActiveByTour(_tour); }


        private ObservableCollection<TourAppointment> _appointments = new();
        public ObservableCollection<TourAppointment> Appointments
        {
            get => _appointments;
            set
            {
                if (_appointments == value) return;
                _appointments = value;
                OnPropertyChanged();
            }
        }

        private TourAppointment _selectedAppointment = new();
        public TourAppointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (value != _selectedAppointment)
                {
                    _selectedAppointment = value;
                    OnPropertyChanged(nameof(SelectedAppointment));
                }
            }
        }

        public AppointmentPickerViewModel(Tour tour)
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tour = tour;
            Appointments = new(_tourAppointmentService.GetAllByTourId(_tour.Id));
            LiveTrackCommand = new RelayCommand(LiveTrackExecute, LiveTrackCanExecute);
        }

        #region LiveTrackCommand
        public ICommand LiveTrackCommand {get; private set;}

        public bool LiveTrackCanExecute()
        {
            return (SelectedAppointment == _active && _active != null) || (SelectedAppointment.TourStatus == Status.INACTIVE && _active == null);
        }
        public void LiveTrackExecute() 
        {
            if (_active != null) return;
            SelectedAppointment = _tourAppointmentService.Activate(SelectedAppointment, _tour);
        }
        #endregion
    }
}