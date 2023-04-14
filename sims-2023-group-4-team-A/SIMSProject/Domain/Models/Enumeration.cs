namespace SIMSProject.Domain.Models
{
    public enum AccommodationType
    {
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
        Waiting = 0,
        Accepted,
        Rejected
    }
}
