using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IOfferService
    {
        Offer Create(Offer Offer);

        bool Delete(Offer Offer);

        Offer Update(Offer Offer);
    }
}
