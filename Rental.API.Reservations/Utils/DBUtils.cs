using Rental.API.Reservations.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Reservations.Utils
{
    public static class DBUtils
    {
        public static void SeedData(ReservationsDbContext dBContext)
        {
            if (!dBContext.Reservations.Any())
            {
            }
        }
    }
}
