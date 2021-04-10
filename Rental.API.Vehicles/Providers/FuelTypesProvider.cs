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
    public class FuelTypesProvider : IFuelTypesProvider
    {
        private readonly VehiclesDBContext dBContext;
        private readonly ILogger<FuelTypesProvider> logger;
        private readonly IMapper mapper;

        public FuelTypesProvider(VehiclesDBContext dBContext, ILogger<FuelTypesProvider> logger, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        private void SeedData()
        {
            if (!dBContext.FuelTypes.Any())
            {
                dBContext.FuelTypes.Add(new DB.FuelType() { ID = 1, FuelTypeName = "Gasolina" });
                dBContext.FuelTypes.Add(new DB.FuelType() { ID = 2, FuelTypeName = "Álcool" });
                dBContext.FuelTypes.Add(new DB.FuelType() { ID = 3, FuelTypeName = "Diesel" });
                dBContext.FuelTypes.Add(new DB.FuelType() { ID = 4, FuelTypeName = "Flex" });
                dBContext.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.FuelType> FuelTypes, string ErrorMessage)> GetFuelTypesAsync()
        {
            try
            {
                var fuelTypes = await dBContext.FuelTypes.ToListAsync();
                if (fuelTypes != null && fuelTypes.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.FuelType>, IEnumerable<Models.FuelType>>(fuelTypes);
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

        public async Task<(bool IsSuccess, Models.FuelType FuelType, string ErrorMessage)> GetFuelTypeAsync(int id)
        {
            try
            {
                var fuelType = await dBContext.FuelTypes.FirstOrDefaultAsync(f => f.ID == id);

                if (fuelType != null)
                {
                    var result = mapper.Map<DB.FuelType, Models.FuelType>(fuelType);
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
