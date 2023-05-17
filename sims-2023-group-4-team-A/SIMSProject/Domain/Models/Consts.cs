namespace SIMSProject.Domain.Models
{
    public class Consts
    {
        public const int GuestRatingDeadline = 5;
        public const int RenovationCancellationDeadline = 5;
        public const int SuperOwnerMinimumRatingCount = 5;
        public const double SuperOwnerMinimumRating = 4.5;

        public const string RateGuestNotificationTitle = "Ocenite gosta!";
        public const string RateGuestNotificationDescription = "Gost @guestUsername je @endDate napustio smeštaj @accommodation. Ostavite ocenu!";
    }
}
