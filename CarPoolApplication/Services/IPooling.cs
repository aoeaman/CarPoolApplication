﻿using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IPooling
    {
        Trip Create(Trip trip,string name);
    }
}
