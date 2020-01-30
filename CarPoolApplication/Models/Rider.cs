using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication.Models
{
    public class Rider:User
    {
        List<string> BookingIDs { get; set; }
    }
}
