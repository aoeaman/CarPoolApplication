﻿using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IBooking
    {
        void ConfirmRide(Booking ride);

        void CancelRide(Booking ride);

        Booking CreateRide(Booking ride);
    }
}
