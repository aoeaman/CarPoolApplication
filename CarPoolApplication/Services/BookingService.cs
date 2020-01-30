using CarPoolApplication.Models;
namespace CarPoolApplication.Services
{
    public class BookingService
    {
        UtilityService Service;
        public BookingService()
        {
            Service = new UtilityService();
        }

        public void ConfirmRide(Ride ride)
        {
            ride.Status = StatusOfRide.Accepted;
        }

        public void CancelRide(Ride ride)
        {
            ride.Status = StatusOfRide.Cancelled;
        }

        public Ride CreateRide(Ride ride)
        {
            ride.ID=Service.GenerateID();
            return ride;
        }
    }
}
