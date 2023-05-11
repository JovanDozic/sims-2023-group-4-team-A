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
        Guest,
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
        MONTHLY = 0, 
        YEARLY 
    }
}
