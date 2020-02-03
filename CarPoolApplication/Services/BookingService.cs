using CarPoolApplication.Models;
namespace CarPoolApplication.Services
{
    public class BookingService:IBookingService
    {
        UtilityService Service;
        public BookingService()
        {
            Service = new UtilityService();
        }

        public void ConfirmRide(Booking ride)
        {
            ride.Status = StatusOfRide.Accepted;
        }

        public void CancelRide(Booking ride)
        {
            ride.Status = StatusOfRide.Cancelled;
        }

        public Booking CreateRide(Booking ride)
        {
            ride.ID=Service.GenerateID();
            return ride;
        }
    }
}
