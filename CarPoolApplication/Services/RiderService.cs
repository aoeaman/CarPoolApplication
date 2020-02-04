using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using Newtonsoft.Json;

namespace CarPoolApplication.Services
{
    public class RiderService:ICommonService<Rider>
    {
        UtilityService Service;
        List<Rider> Riders;
        public readonly string RiderPath = "C:\\repos\\CarPoolApplication\\CarPoolApplication\\Rider.JSON";
        public RiderService()
        {
            Service = new UtilityService();
            Riders= JsonConvert.DeserializeObject<List<Rider>>(File.ReadAllText(RiderPath)) ?? new List<Rider>();
        }

        public Rider Create(Rider rider)
        {
            rider.ID = Service.GenerateID();
            return rider;
        }

        public void Add(Rider rider)
        {
            Riders.Add(rider);
        }

        public List<Rider> GetAll()
        {
            return Riders;
        }
    }
}
