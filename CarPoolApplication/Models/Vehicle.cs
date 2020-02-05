namespace CarPoolApplication.Models
{
    public class Vehicle
    {
        public string ID { get; set; }
        public string Maker { get; set; }
        public string Number { get; set; }
        public string DriverID { get; set; }
        public byte Seats { get; set; }
        public bool IsActive { get; set; }
        public VehicleType Type { get; set; }
    }
}
