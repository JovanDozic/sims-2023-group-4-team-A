﻿using SIMSProject.Application.DTOs;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using System;

namespace SIMSProject.WPF.Messenger.Messages
{
    public class LiveTrackMessage : Message
    {
        public TourAppointment Appointment { get; set; }
        public LiveTrackMessage(object sender, TourAppointment appointment) : base(sender)
        {
            Appointment = appointment;
        }
    }
    public class TourInfoMessage : Message
    {
        public Tour Tour { get; set; }
        public TourInfoMessage(object sender, Tour tour) : base(sender)
        {
            Tour = tour;
        }
    }
    public class DetailedReviewMessage : Message
    {
        public TourAppointmentRatingDTO Rating { get; set; }
        public DetailedReviewMessage(object sender, TourAppointmentRatingDTO ratingDTO) : base(sender)
        {
            Rating = ratingDTO;
        }
    }
    public class ScheduleRequestMessage : Message
    {
        public CustomTourRequest Request { get; set; }
        public ScheduleRequestMessage(object sender, CustomTourRequest request) : base(sender)
        {
            Request = request;
        }
    }
    public class CreateRequestedMessage : Message
    {
        public CustomTourRequest Request { get; set; }
        public DateTime Appointment { get; set; }
        public int Duration { get; set; }
        public CreateRequestedMessage(object sender, CustomTourRequest request, DateTime appointment) : base(sender)
        {
            Request = request;
            Appointment = appointment;
        }
        public CreateRequestedMessage(object sender, CustomTourRequest request, DateTime appointment, int duration) : base(sender)
        {
            Request = request;
            Appointment = appointment;
            Duration = duration;
        }
    }
    public class CreateMostWantedMessage : Message
    {
        public Language Language { get; set; }
        public Location? Location { get; set; }
        public CreateMostWantedMessage(object sender, Language language) : base(sender)
        {
            Language = language;
        }
        public CreateMostWantedMessage(object sender, Location location) : base(sender)
        {
            Location = location;
        }
    }
}
