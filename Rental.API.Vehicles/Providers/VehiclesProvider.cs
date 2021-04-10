using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rental.API.Vehicles.DB;
using Rental.API.Vehicles.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Providers
{
    public class VehiclesProvider : IVehicleProvider
    {
        private readonly VehiclesDBContext dBContext;
        private readonly ILogger<VehiclesProvider> logger;
        private readonly IMapper mapper;

        public VehiclesProvider(VehiclesDBContext dBContext, ILogger<VehiclesProvider> logger, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }
        private void SeedData()
        {
            if (!dBContext.Vehicles.Any())
            {
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 1, CarModelID = 1, FuelTypeID = 4, LicensePlate = "BRK-3D21", ModelYear = 2021, RentalPricePerHour = 10, TrunkLimit = 2 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 2, CarModelID = 2, FuelTypeID = 1, LicensePlate = "AWN-2F73", ModelYear = 2021, RentalPricePerHour = 25, TrunkLimit = 4 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 3, CarModelID = 3, FuelTypeID = 1, LicensePlate = "BND-1A93", ModelYear = 2021, RentalPricePerHour = 40, TrunkLimit = 4 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 4, CarModelID = 4, FuelTypeID = 1, LicensePlate = "AJJ-3G62", ModelYear = 2021, RentalPricePerHour = 50, TrunkLimit = 4 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 5, CarModelID = 5, FuelTypeID = 4, LicensePlate = "EDB-2K34", ModelYear = 2021, RentalPricePerHour = 7, TrunkLimit = 2 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 6, CarModelID = 6, FuelTypeID = 3, LicensePlate = "AEW-7H08", ModelYear = 2021, RentalPricePerHour = 12, TrunkLimit = 6 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 7, CarModelID = 7, FuelTypeID = 3, LicensePlate = "BDN-6D54", ModelYear = 2021, RentalPricePerHour = 12, TrunkLimit = 6 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 8, CarModelID = 8, FuelTypeID = 2, LicensePlate = "DEK-8D02", ModelYear = 2021, RentalPricePerHour = 4, TrunkLimit = 1 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 9, CarModelID = 9, FuelTypeID = 4, LicensePlate = "MRD-6A12", ModelYear = 2021, RentalPricePerHour = 5, TrunkLimit = 1 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 10, CarModelID = 10, FuelTypeID = 1, LicensePlate = "PNJ-5D44", ModelYear = 2021, RentalPricePerHour = 9, TrunkLimit = 3 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 11, CarModelID = 11, FuelTypeID = 1, LicensePlate = "AJH-7K33", ModelYear = 2021, RentalPricePerHour = 15, TrunkLimit = 4 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 12, CarModelID = 12, FuelTypeID = 1, LicensePlate = "AKM-7G98", ModelYear = 2021, RentalPricePerHour = 20, TrunkLimit = 6 });
                dBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.ViewModels.Vehicle> Vehicles, string ErrorMessage)> GetVehiclesAsync()
        {
            try
            {
                var vehicles = await dBContext.Vehicles.Include(m => m.CarModel).Include(m => m.CarModel.Make).Include(m => m.CarModel.VehicleCategory).Include(f => f.FuelType).ToListAsync();
                if (vehicles != null && vehicles.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Vehicle>, IEnumerable<Models.ViewModels.Vehicle>>(vehicles);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.ViewModels.Vehicle Vehicle, string ErrorMessage)> GetVehicleAsync(int id)
        {
            try
            {
                var vehicle = await dBContext.Vehicles.Include(m => m.CarModel).Include(m => m.CarModel.Make).Include(m => m.CarModel.VehicleCategory).Include(f => f.FuelType).FirstOrDefaultAsync(m => m.ID == id);

                if (vehicle != null)
                {
                    var result = mapper.Map<DB.Vehicle, Models.ViewModels.Vehicle>(vehicle);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
