using Rental.API.Reservations.DB;
using System;
using System.Linq;

namespace Rental.API.Reservations.Utils
{
    public static class DBUtils
    {
        public static void SeedData(ReservationsDbContext dBContext)
        {
            if (!dBContext.Reservations.Any())
            {
                dBContext.Reservations.Add(new DB.Reservation() 
                { 
                    CPF = "00000000000",
                    VehicleID = 2,
                    RentalPricePerHour = 25,
                    StartDate = new DateTime(2021, 1, 1),
                    EndDate = new DateTime(2021, 1, 10),
                    IsCarReturned = true,
                    IsCarClean = true,
                    IsCarDamaged = false,
                    IsFuelTankFull = true,
                    RentalPricePerHourAfterReturn = 25
                });
                dBContext.Reservations.Add(new DB.Reservation()
                {
                    CPF = "00000000000",
                    VehicleID = 1,
                    RentalPricePerHour = 10,
                    StartDate = new DateTime(2021, 4, 12),
                    EndDate = new DateTime(2021, 4, 20),
                    IsCarReturned = false,
                    IsCarClean = true,
                    IsCarDamaged = false,
                    IsFuelTankFull = true,
                    RentalPricePerHourAfterReturn = 10
                });
                dBContext.Reservations.Add(new DB.Reservation()
                {
                    CPF = "00000000000",
                    VehicleID = 5,
                    RentalPricePerHour = 7,
                    StartDate = new DateTime(2021, 5, 12),
                    EndDate = new DateTime(2021, 5, 20),
                    IsCarReturned = false,
                    IsCarClean = true,
                    IsCarDamaged = false,
                    IsFuelTankFull = true,
                    RentalPricePerHourAfterReturn = 7
                });
                dBContext.Reservations.Add(new DB.Reservation()
                {
                    CPF = "00000000000",
                    VehicleID = 8,
                    RentalPricePerHour = 4,
                    StartDate = new DateTime(2021, 5, 12),
                    EndDate = DateTime.Now,
                    IsCarReturned = false,
                    IsCarClean = true,
                    IsCarDamaged = false,
                    IsFuelTankFull = true,
                    RentalPricePerHourAfterReturn = 4
                });
                dBContext.Reservations.Add(new DB.Reservation()
                {
                    CPF = "00000000001",
                    VehicleID = 10,
                    RentalPricePerHour = 9,
                    StartDate = new DateTime(2021, 4, 12),
                    EndDate = new DateTime(2021, 4, 18),
                    IsCarReturned = false,
                    IsCarClean = true,
                    IsCarDamaged = false,
                    IsFuelTankFull = true,
                    RentalPricePerHourAfterReturn = 9
                });
                dBContext.Reservations.Add(new DB.Reservation()
                {
                    CPF = "00000000002",
                    VehicleID = 4,
                    RentalPricePerHour = 50,
                    StartDate = new DateTime(2021, 4, 15),
                    EndDate = new DateTime(2021, 4, 25),
                    IsCarReturned = false,
                    IsCarClean = true,
                    IsCarDamaged = false,
                    IsFuelTankFull = true,
                    RentalPricePerHourAfterReturn = 50
                });
                dBContext.SaveChanges();
            }
        }
    }
}
