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
    public class VehicleCategoriesProvider : IVehicleCategoriesProvider
    {
        private readonly VehiclesDBContext dBContext;
        private readonly ILogger<VehicleCategoriesProvider> logger;
        private readonly IMapper mapper;

        public VehicleCategoriesProvider(VehiclesDBContext dBContext, ILogger<VehicleCategoriesProvider> logger, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dBContext.VehicleCategories.Any())
            {
                dBContext.VehicleCategories.Add(new DB.VehicleCategory() { ID = 1, CategoryName = "Básico" });
                dBContext.VehicleCategories.Add(new DB.VehicleCategory() { ID = 2, CategoryName = "Completo" });
                dBContext.VehicleCategories.Add(new DB.VehicleCategory() { ID = 3, CategoryName = "Luxo" });
                dBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.VehicleCategory> VehicleCategories, string ErrorMessage)> GetVehicleCategoriesAsync()
        {
            try
            {
                var vehicleCategories = await dBContext.VehicleCategories.ToListAsync();
                if (vehicleCategories != null && vehicleCategories.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.VehicleCategory>, IEnumerable<Models.VehicleCategory>>(vehicleCategories);
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

        public async Task<(bool IsSuccess, Models.VehicleCategory VehicleCategory, string ErrorMessage)> GetVehicleCategoryAsync(int id)
        {
            try
            {
                var vehicleCategory = await dBContext.VehicleCategories.FirstOrDefaultAsync(m => m.ID == id);

                if (vehicleCategory != null)
                {
                    var result = mapper.Map<DB.VehicleCategory, Models.VehicleCategory>(vehicleCategory);
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
