using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rental.API.Vehicles.DB;
using Rental.API.Vehicles.Interfaces;
using Rental.API.Vehicles.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Providers
{
    public class VehiclesProvider : IVehiclesProvider
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
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 1, CarModelID = 1,  LicensePlate = "BRK-3D21", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 2, CarModelID = 2,  LicensePlate = "AWN-2F73", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 3, CarModelID = 3,  LicensePlate = "BND-1A93", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 4, CarModelID = 4,  LicensePlate = "AJJ-3G62", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 5, CarModelID = 5,  LicensePlate = "EDB-2K34", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 6, CarModelID = 6,  LicensePlate = "AEW-7H08", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 7, CarModelID = 7,  LicensePlate = "BDN-6D54", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 8, CarModelID = 8,  LicensePlate = "DEK-8D02", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 9, CarModelID = 9,  LicensePlate = "MRD-6A12", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 10, CarModelID = 10, LicensePlate = "PNJ-5D44", ModelYear = 2021});
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 11, CarModelID = 11, LicensePlate = "AJH-7K33", ModelYear = 2021 });
                dBContext.Vehicles.Add(new DB.Vehicle() { ID = 12, CarModelID = 12, LicensePlate = "AKM-7G98", ModelYear = 2021 });
                dBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.ViewModels.Vehicle> Vehicles, string ErrorMessage)> GetVehiclesAsync()
        {
            try
            {
                var vehicles = await dBContext.Vehicles.Include(m => m.CarModel).Include(m => m.CarModel.Make).Include(m => m.CarModel.VehicleCategory).ToListAsync();
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
                var vehicle = await dBContext.Vehicles.Include(m => m.CarModel).Include(m => m.CarModel.Make).Include(m => m.CarModel.VehicleCategory).FirstOrDefaultAsync(v => v.ID == id);

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

        public async Task<(bool IsSuccess, Models.ViewModels.Vehicle Vehicle, string ErrorMessage)> PostVehicleAsync(VehicleRequest vehicle)
        {
            try
            {
                var newVehicle = new DB.Vehicle()
                {
                    CarModelID = vehicle.CarModelID,                    
                    LicensePlate = vehicle.LicensePlate,
                    ModelYear = vehicle.ModelYear                    
                };
                dBContext.Add(newVehicle);
                if (await dBContext.SaveChangesAsync() > 0)
                {
                    newVehicle = await dBContext.Vehicles.Include(m => m.CarModel).Include(m => m.CarModel.Make).Include(m => m.CarModel.VehicleCategory).FirstOrDefaultAsync(v => v.ID == newVehicle.ID);
                    var result = mapper.Map<DB.Vehicle, Models.ViewModels.Vehicle>(newVehicle);
                    return (true, result, null);
                }
                return (false, null, "Failed to insert record.");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.ViewModels.Vehicle Vehicle, string ErrorMessage)> DeleteVehicleAsync(int id)
        {
            try
            {
                var vehicle = new DB.Vehicle() { ID = id };
                dBContext.Remove(vehicle);
                if (await dBContext.SaveChangesAsync() > 0)
                {
                    return (true, null, null);
                }
                return (false, null, "Failed to delete record.");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.ViewModels.Vehicle Vehicle, string ErrorMessage)> PutVehicleAsync(VehicleUpdateRequest vehicle)
        {
            try
            {
                var entity = await dBContext.Vehicles.FirstOrDefaultAsync(v => v.ID == vehicle.ID);
                if (entity != null)
                {
                    entity.CarModelID = vehicle.CarModelID;
                    entity.LicensePlate = string.IsNullOrEmpty(vehicle.LicensePlate) ? entity.LicensePlate : vehicle.LicensePlate;
                    entity.ModelYear = vehicle.ModelYear;

                    dBContext.Update(entity);

                    if (await dBContext.SaveChangesAsync() > 0)
                    {
                        entity = await dBContext.Vehicles.Include(m => m.CarModel).Include(m => m.CarModel.Make).Include(m => m.CarModel.VehicleCategory).FirstOrDefaultAsync(v => v.ID == entity.ID);
                        var result = mapper.Map<DB.Vehicle, Models.ViewModels.Vehicle>(entity);
                        return (true, result, null);
                    }
                    return (false, null, "Failed to update record.");
                }
                return (false, null, "Montadora não encontrada.");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }    
}
