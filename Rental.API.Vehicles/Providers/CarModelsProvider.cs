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
                dBContext.CarModels.Add(new DB.CarModel() { ID = 1, MakeID = 1, Name = "A3", VehicleCategoryID = 2 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 2, MakeID = 1, Name = "A4", VehicleCategoryID = 2 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 3, MakeID = 2, Name = "M3", VehicleCategoryID = 3 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 4, MakeID = 2, Name = "M5", VehicleCategoryID = 3 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 5, MakeID = 3, Name = "Onix", VehicleCategoryID = 1 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 6, MakeID = 3, Name = "S10", VehicleCategoryID = 2 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 7, MakeID = 4, Name = "Toro", VehicleCategoryID = 2 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 8, MakeID = 4, Name = "Uno", VehicleCategoryID = 1 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 9, MakeID = 5, Name = "Fiesta", VehicleCategoryID = 1 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 10, MakeID = 5, Name = "Focus", VehicleCategoryID = 2 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 11, MakeID = 6, Name = "Jetta", VehicleCategoryID = 2 });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 12, MakeID = 6, Name = "Tiguan", VehicleCategoryID = 2 });
                dBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.ViewModels.CarModel> CarModels, string ErrorMessage)> GetCarModelsAsync()
        {
            try
            {
                var carModels = await dBContext.CarModels.Include(m => m.Make).Include(c => c.VehicleCategory).ToListAsync();
                if (carModels != null && carModels.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.CarModel>, IEnumerable<Models.ViewModels.CarModel>>(carModels);
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

        public async Task<(bool IsSuccess, Models.ViewModels.CarModel CarModel, string ErrorMessage)> GetCarModelAsync(int id)
        {
            try
            {
                var carModel = await dBContext.CarModels.Include(m => m.Make).Include(c => c.VehicleCategory).FirstOrDefaultAsync(m => m.ID == id);

                if (carModel != null)
                {
                    var result = mapper.Map<DB.CarModel, Models.ViewModels.CarModel>(carModel);
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

        public async Task<(bool IsSuccess, Models.ViewModels.CarModel CarModel, string ErrorMessage)> PostCarModelAsync(Models.RequestModels.CarModelRequest carModel)
        {
            try
            {
                var newCarModel = new DB.CarModel()
                {
                    MakeID = carModel.MakeID,
                    Name = carModel.Name,
                    VehicleCategoryID = carModel.VehicleCategoryID
                };
                dBContext.Add(newCarModel);
                if (await dBContext.SaveChangesAsync() > 0)
                {
                    newCarModel = await dBContext.CarModels.Include(m => m.Make).Include(c => c.VehicleCategory).FirstOrDefaultAsync(m => m.ID == newCarModel.ID);
                    var result = mapper.Map<DB.CarModel, Models.ViewModels.CarModel>(newCarModel);
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

        public async Task<(bool IsSuccess, Models.ViewModels.CarModel CarModel, string ErrorMessage)> DeleteCarModelAsync(int id)
        {
            try
            {
                var carModel = new DB.CarModel() { ID = id };
                dBContext.Remove(carModel);
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

        public async Task<(bool IsSuccess, Models.ViewModels.CarModel CarModel, string ErrorMessage)> PutCarModelAsync(Models.RequestModels.CarModelUpdateRequest carModel)
        {
            try
            {
                var entity = await dBContext.CarModels.FirstOrDefaultAsync(m => m.ID == carModel.ID);
                if (entity != null)
                {
                    entity.Name = carModel.Name;
                    entity.MakeID = carModel.MakeID;
                    entity.VehicleCategoryID = carModel.VehicleCategoryID;

                    dBContext.Update(entity);
                    if (await dBContext.SaveChangesAsync() > 0)
                    {
                        var result = mapper.Map<DB.CarModel, Models.ViewModels.CarModel>(entity);
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
