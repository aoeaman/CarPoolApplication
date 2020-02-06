using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IOfferService
    {

        Offer Update(Offer Offer);
        void Delete(string offerID);
        List<Offer> GetAll();
        Offer Create(Offer offer);
        void Add(Offer offer);
        Offer GetByID(string offerID);
        void SaveData();
    }
}
