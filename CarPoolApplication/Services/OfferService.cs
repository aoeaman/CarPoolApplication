using System;
using System.Collections.Generic;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public class OfferService:IOfferService
    {
        UtilityService Service;

        public OfferService()
        {
            Service = new UtilityService();
        }

        public bool Delete(List<Offer> Offers,Offer Offer)
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
    }
}
