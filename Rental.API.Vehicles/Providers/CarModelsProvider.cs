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
    public class CarModelsProvider : ICarModelsProvider
    {
        private readonly VehiclesDBContext dBContext;
        private readonly ILogger<CarModelsProvider> logger;
        private readonly IMapper mapper;

        public CarModelsProvider(VehiclesDBContext dBContext, ILogger<CarModelsProvider> logger, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dBContext.CarModels.Any())
            {
                dBContext.CarModels.Add(new DB.CarModel() { ID = 1, MakeID = 1, Name = "A3" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 2, MakeID = 1, Name = "A4" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 3, MakeID = 2, Name = "M3" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 4, MakeID = 2, Name = "M5" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 5, MakeID = 3, Name = "Onix" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 6, MakeID = 3, Name = "S10" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 7, MakeID = 4, Name = "Toro" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 8, MakeID = 4, Name = "Uno" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 9, MakeID = 5, Name = "Fiesta" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 10, MakeID = 5, Name = "Focus" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 11, MakeID = 6, Name = "Jetta" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 12, MakeID = 6, Name = "Tiguan" });
                dBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.CarModel> CarModels, string ErrorMessage)> GetCarModelsAsync()
        {
            try
            {
                var carModels = await dBContext.CarModels.Include(m => m.Make).ToListAsync();
                if (carModels != null && carModels.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.CarModel>, IEnumerable<Models.CarModel>>(carModels);
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

        public async Task<(bool IsSuccess, Models.CarModel CarModel, string ErrorMessage)> GetCarModelAsync(int id)
        {
            try
            {
                var carModel = await dBContext.CarModels.Include(m => m.Make).FirstOrDefaultAsync(m => m.ID == id);

                if (carModel != null)
                {
                    var result = mapper.Map<DB.CarModel, Models.CarModel>(carModel);
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
