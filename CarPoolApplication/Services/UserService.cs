using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public class UserService:IUserService
    {
        UtilityService Service;

        public UserService()
        {
            Service = new UtilityService();
        }

        public Driver CreateDriver(Driver driver)
        {
            driver.ID = Service.GenerateID();
            return driver;
        }

        public Rider CreateRider(Rider rider)
        {
            rider.ID = Service.GenerateID();
            return rider;
        }

        public Vehicle RegisterUserVehicle(Vehicle vehicle)
        {
            vehicle.ID = Service.GenerateID();
            return vehicle;
        }
    }
}
