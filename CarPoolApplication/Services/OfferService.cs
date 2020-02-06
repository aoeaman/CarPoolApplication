using System;
using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using Newtonsoft.Json;
using CarPoolApplication.Services.Interfaces;

namespace CarPoolApplication.Services
{
    public class OfferService:IOfferService
    {
        UtilityService Service;
        List<Offer> Offers;
        public readonly string OfferPath = "C:\\repos\\CarPoolApplication\\CarPoolApplication\\Trips.JSON";
        public OfferService()
        {
            Service = new UtilityService();
            Offers= JsonConvert.DeserializeObject<List<Offer>>(File.ReadAllText(OfferPath)) ?? new List<Offer>();
        }

        public void Add(Offer offer)
        {
            Offers.Add(offer);
        }

        
        public Offer Create(Offer Offer)
        {
            Offer.ID = Service.GenerateID();
            return Offer;
        }

        public List<Offer> GetAll()
        {
            return Offers;
        }

        public void Delete(string iD)
        {
            Offers.Remove(Offers.Find(_=>_.ID==iD));
        }

        public Offer GetByID(string offerID)
        {
            return Offers.Find(Name => Name.ID == offerID);
        }

        public Offer Update(Offer Offer)
        {
            throw new NotImplementedException();
        }

        public void SaveData()
        {
            File.WriteAllText(OfferPath, JsonConvert.SerializeObject(Offers));
        }
    }
}
