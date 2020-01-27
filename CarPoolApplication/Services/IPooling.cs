using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    interface IPooling
    {
        Trip Create(Trip trip);
    }
}
