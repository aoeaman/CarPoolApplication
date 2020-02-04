namespace CarPoolApplication.Models
{
    public class Booking
    {
        public string ID { get; set; }
        public StatusOfRide Status { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public string RiderID { get; set; }
        public string OfferID { get; set; }
        public decimal Fare { get; set; }
        public byte Seats { get; set; }
    }
}
