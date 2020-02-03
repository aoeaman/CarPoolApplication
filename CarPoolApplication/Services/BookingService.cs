using System.Collections.Generic;
using CarPoolApplication.Models;
namespace CarPoolApplication.Services
{
    public class BookingService:IBookingService,ICommonService<Booking>
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

        public void Add(Booking entity)
        {
            throw new System.NotImplementedException();
        }

        public Booking Create(Booking entity)
        {
            throw new System.NotImplementedException();
        }

        public IList<Booking> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
