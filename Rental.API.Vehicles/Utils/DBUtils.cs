using Rental.API.Vehicles.DB;
using System.Linq;

namespace Rental.API.Vehicles.Utils
{
    public static class DBUtils
    {
        public static void SeedData(VehiclesDBContext dBContext)
        {
            if (!dBContext.Makes.Any())
            {
                dBContext.Makes.Add(new DB.Make() { ID = 1, Name = "Audi" });
                dBContext.Makes.Add(new DB.Make() { ID = 2, Name = "BMW" });
                dBContext.Makes.Add(new DB.Make() { ID = 3, Name = "Chevrolet" });
                dBContext.Makes.Add(new DB.Make() { ID = 4, Name = "Fiat" });
                dBContext.Makes.Add(new DB.Make() { ID = 5, Name = "Ford" });
                dBContext.Makes.Add(new DB.Make() { ID = 6, Name = "Volkswagen" });
                dBContext.SaveChanges();
            }

            if (!dBContext.VehicleCategories.Any())
            {
                dBContext.VehicleCategories.Add(new DB.VehicleCategory() { ID = 1, CategoryName = "Básico" });
                dBContext.VehicleCategories.Add(new DB.VehicleCategory() { ID = 2, CategoryName = "Completo" });
                dBContext.VehicleCategories.Add(new DB.VehicleCategory() { ID = 3, CategoryName = "Luxo" });
                dBContext.SaveChanges();
            }

            if (!dBContext.FuelTypes.Any())
            {
                dBContext.FuelTypes.Add(new DB.FuelType() { ID = 1, FuelTypeName = "Gasolina" });
                dBContext.FuelTypes.Add(new DB.FuelType() { ID = 2, FuelTypeName = "Álcool" });
                dBContext.FuelTypes.Add(new DB.FuelType() { ID = 3, FuelTypeName = "Diesel" });
                dBContext.FuelTypes.Add(new DB.FuelType() { ID = 4, FuelTypeName = "Flex" });
                dBContext.SaveChanges();
            }

            if (!dBContext.CarModels.Any())
            {
                dBContext.CarModels.Add(new DB.CarModel() { ID = 1, FuelTypeID = 4, RentalPricePerHour = 10, MakeID = 1, Name = "A3", VehicleCategoryID = 2, TrunkLimit = 2 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 2, FuelTypeID = 1, RentalPricePerHour = 25, MakeID = 1, Name = "A4", VehicleCategoryID = 2, TrunkLimit = 4 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 3, FuelTypeID = 1, RentalPricePerHour = 40, MakeID = 2, Name = "M3", VehicleCategoryID = 3, TrunkLimit = 4 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 4, FuelTypeID = 1, RentalPricePerHour = 50, MakeID = 2, Name = "M5", VehicleCategoryID = 3, TrunkLimit = 4 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 5, FuelTypeID = 4, RentalPricePerHour = 7, MakeID = 3, Name = "Onix", VehicleCategoryID = 1, TrunkLimit = 2 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 6, FuelTypeID = 3, RentalPricePerHour = 12, MakeID = 3, Name = "S10", VehicleCategoryID = 2, TrunkLimit = 6 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 7, FuelTypeID = 3, RentalPricePerHour = 12, MakeID = 4, Name = "Toro", VehicleCategoryID = 2, TrunkLimit = 6 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 8, FuelTypeID = 2, RentalPricePerHour = 4, MakeID = 4, Name = "Uno", VehicleCategoryID = 1, TrunkLimit = 1 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 9, FuelTypeID = 4, RentalPricePerHour = 5, MakeID = 5, Name = "Fiesta", VehicleCategoryID = 1, TrunkLimit = 1 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 10, FuelTypeID = 1, RentalPricePerHour = 9, MakeID = 5, Name = "Focus", VehicleCategoryID = 2, TrunkLimit = 3 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 11, FuelTypeID = 1, RentalPricePerHour = 15, MakeID = 6, Name = "Jetta", VehicleCategoryID = 2, TrunkLimit = 4 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 12, FuelTypeID = 1, RentalPricePerHour = 20, MakeID = 6, Name = "Tiguan", VehicleCategoryID = 2, TrunkLimit = 6 });
                dBContext.SaveChanges();
            }

            if (!dBContext.Vehicles.Any())
            {
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 1, CarModelID = 1, LicensePlate = "BRK-3D21", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 2, CarModelID = 2, LicensePlate = "AWN-2F73", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 3, CarModelID = 3, LicensePlate = "BND-1A93", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 4, CarModelID = 4, LicensePlate = "AJJ-3G62", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 5, CarModelID = 5, LicensePlate = "EDB-2K34", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 6, CarModelID = 6, LicensePlate = "AEW-7H08", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 7, CarModelID = 7, LicensePlate = "BDN-6D54", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 8, CarModelID = 8, LicensePlate = "DEK-8D02", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 9, CarModelID = 9, LicensePlate = "MRD-6A12", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 10, CarModelID = 10, LicensePlate = "PNJ-5D44", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 11, CarModelID = 11, LicensePlate = "AJH-7K33", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 12, CarModelID = 12, LicensePlate = "AKM-7G98", ModelYear = 2021 });
                dBContext.SaveChanges();
            }
                       
        }
    }
}
