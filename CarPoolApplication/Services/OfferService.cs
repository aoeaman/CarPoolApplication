using System;
using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using Newtonsoft.Json;

namespace CarPoolApplication.Services
{
    public class OfferService:IOfferService,ICommonService<Offer>
    {
        UtilityService Service;
        IList<Offer> Offers;
        public readonly string OfferPath = "C:\\repos\\CarPoolApplication\\CarPoolApplication\\Trips.JSON";
        public OfferService()
        {
            Service = new UtilityService();
            Offers= JsonConvert.DeserializeObject<List<Offer>>(File.ReadAllText(OfferPath));
        }

        public void Add(Offer offer)
        {
            Offers.Add(offer);
        }

        public bool Delete(Offer Offer)
        {
            try
            {
                Offers.Remove(Offer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }       

        public Offer Create(Offer Offer)
        {
            Offer.ID = Service.GenerateID();
            return Offer;
        }

        public Offer Update(Offer Offer)
        {
            throw new NotImplementedException();
        }

        public IList<Offer> GetAll()
        {
            return Offers;
        }
    }
}
