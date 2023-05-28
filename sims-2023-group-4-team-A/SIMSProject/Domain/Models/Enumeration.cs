namespace SIMSProject.Domain.Models
{
    public enum AccommodationType
    {
        None = -1,
        Apartment = 0,
        House,
        Hut
    }
    public enum UserRole
    {
        Owner = 0,
        Guide,
        Guest1,
        Guest2,
        SuperOwner,
        SuperGuide,
        SuperGuest
    }
    public enum ReschedulingRequestStatus
    {
        None = 0,
        Waiting,
        Accepted,
        Rejected
    }

    public enum AccommodationStatisticType 
    { 
        Monthly = 0, 
        Yearly 
    }
}
