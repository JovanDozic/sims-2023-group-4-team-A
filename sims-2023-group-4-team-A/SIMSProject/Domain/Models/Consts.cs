namespace SIMSProject.Domain.Models
{
    public static class Consts
    {
        public const int GuestRatingDeadline = 5;
        public const int RenovationCancellationDeadline = 5;
        public const int SuperOwnerMinimumRatingCount = 5;
        public const double SuperOwnerMinimumRating = 4.5;

        public const string RateGuestNotificationTitle = "Ocenite gosta!";
        public const string RateGuestNotificationDescription = "Gost @guestUsername je @endDate napustio smeštaj @_accommodation. Ostavite ocenu!";

        public const string ForumCreatedInOwnersLocationTitle = "Novi forum!";
        public const string ForumCreatedInOwnersLocationDescription = "Korisnik @username je kreirao forum za lokaciju @location!";

        public static int UsefulForumOwnerCommentsCount = 2;
        public static int UsefulForumGuestCommentsCount = 5;
    }
}
