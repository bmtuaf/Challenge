using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rental.API.Vehicles.DB;
using Rental.API.Vehicles.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Providers
{
    public class CarModelsProvider : ICarModelsProvider
    {
        private readonly VehiclesDBContext dBContext;
        private readonly ILogger<CarModelsProvider> logger;
        private readonly IMapper mapper;
        public static IWebHostEnvironment _environment;

        public CarModelsProvider(VehiclesDBContext dBContext, ILogger<CarModelsProvider> logger, IMapper mapper, IWebHostEnvironment environment)
        {
            this.dBContext = dBContext;
            this.logger = logger;
            this.mapper = mapper;
            _environment = environment;
            SeedData();
        }

        private void SeedData()
        {
            if (!dBContext.CarModels.Any())
            {
                dBContext.CarModels.Add(new DB.CarModel() { ID = 1, FuelTypeID = 4, RentalPricePerHour = 10, MakeID = 1, Name = "A3", VehicleCategoryID = 2, TrunkLimit = 2, ImagePath = "\\images\\Audi_A3.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 2, FuelTypeID = 1, RentalPricePerHour = 25, MakeID = 1, Name = "A4", VehicleCategoryID = 2, TrunkLimit = 4, ImagePath = "\\images\\Audi_A4.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 3, FuelTypeID = 1, RentalPricePerHour = 40, MakeID = 2, Name = "M3", VehicleCategoryID = 3, TrunkLimit = 4, ImagePath = "\\images\\BMW_M3.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 4, FuelTypeID = 1, RentalPricePerHour = 50, MakeID = 2, Name = "M5", VehicleCategoryID = 3, TrunkLimit = 4, ImagePath = "\\images\\BMW_M5.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 5, FuelTypeID = 4, RentalPricePerHour = 7, MakeID = 3, Name = "Onix", VehicleCategoryID = 1, TrunkLimit = 2, ImagePath = "\\images\\Chevrolet_Onix.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 6, FuelTypeID = 3, RentalPricePerHour = 12, MakeID = 3, Name = "S10", VehicleCategoryID = 2, TrunkLimit = 6, ImagePath = "\\images\\Chevrolet_S10.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 7, FuelTypeID = 3, RentalPricePerHour = 12, MakeID = 4, Name = "Toro", VehicleCategoryID = 2, TrunkLimit = 6, ImagePath = "\\images\\Fiat_Toro.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 8, FuelTypeID = 2, RentalPricePerHour = 4, MakeID = 4, Name = "Uno", VehicleCategoryID = 1, TrunkLimit = 1, ImagePath = "\\images\\Fiat_Uno.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 9, FuelTypeID = 4, RentalPricePerHour = 5, MakeID = 5, Name = "Fiesta", VehicleCategoryID = 1, TrunkLimit = 1, ImagePath = "\\images\\Ford_Fiesta.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 10, FuelTypeID = 1, RentalPricePerHour = 9, MakeID = 5, Name = "Focus", VehicleCategoryID = 2, TrunkLimit = 3, ImagePath = "\\images\\Ford_Focus.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 11, FuelTypeID = 1, RentalPricePerHour = 15, MakeID = 6, Name = "Jetta", VehicleCategoryID = 2, TrunkLimit = 4, ImagePath = "\\images\\VW_Jetta.jpg" });
                dBContext.CarModels.Add(new DB.CarModel() { ID = 12, FuelTypeID = 1, RentalPricePerHour = 20, MakeID = 6, Name = "Tiguan", VehicleCategoryID = 2, TrunkLimit = 6, ImagePath = "\\images\\VW_Tiguan.jpg" });
                dBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.ViewModels.CarModel> CarModels, string ErrorMessage)> GetCarModelsAsync()
        {
            try
            {
                var carModels = await dBContext.CarModels.Include(m => m.Make).Include(f => f.FuelType).Include(c => c.VehicleCategory).ToListAsync();
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
                var carModel = await dBContext.CarModels.Include(m => m.Make).Include(f => f.FuelType).Include(c => c.VehicleCategory).FirstOrDefaultAsync(m => m.ID == id);

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
                    VehicleCategoryID = carModel.VehicleCategoryID,
                    FuelTypeID = carModel.FuelTypeID,
                    TrunkLimit = carModel.TrunkLimit,
                    RentalPricePerHour = carModel.RentalPricePerHour
                };
                dBContext.Add(newCarModel);
                if (await dBContext.SaveChangesAsync() > 0)
                {
                    newCarModel = await dBContext.CarModels.Include(m => m.Make).Include(c => c.VehicleCategory).Include(f => f.FuelType).FirstOrDefaultAsync(m => m.ID == newCarModel.ID);
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
                    entity.RentalPricePerHour = carModel.RentalPricePerHour;
                    entity.FuelTypeID = carModel.FuelTypeID;
                    entity.TrunkLimit = carModel.TrunkLimit;

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

        public async Task<(bool IsSuccess, string ImagePath, string ErrorMessage)> PostCarModelImageAsync(IFormFile file, int carModelID)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\images\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\images\\");
                    }
                    using (FileStream filestream = File.Create(_environment.WebRootPath + "\\images\\" + file.FileName))
                    {
                        await file.CopyToAsync(filestream);
                        var carModel = await dBContext.CarModels.FirstOrDefaultAsync(c => c.ID == carModelID);
                        carModel.ImagePath = $"\\images\\{file.FileName}";
                        dBContext.Update(carModel);
                        if (await dBContext.SaveChangesAsync() > 0)
                        {
                            filestream.Flush();
                            return (true, $"\\images\\{file.FileName}", null);
                        }
                        else
                        {
                            return (false, null, "Ocorreu uma falha no envio do arquivo.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex.ToString());
                    return (false, null, ex.Message);
                }
            }
            else
            {
                return (false, null, "Ocorreu uma falha no envio do arquivo.");                
            }
        }

        public async Task<(bool IsSuccess, byte[] Image, string ErrorMessage)> GetCarModelImageAsync(int carModelID)
        {
            try
            {
                var carModel = await dBContext.CarModels.FirstOrDefaultAsync(m => m.ID == carModelID);

                if (carModel != null)
                {
                    Byte[] b = File.ReadAllBytes(_environment.WebRootPath + carModel.ImagePath);
                    return (true, b, null);
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
