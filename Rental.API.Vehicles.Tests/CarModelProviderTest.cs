using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Rental.API.Vehicles.DB;
using Rental.API.Vehicles.Profiles;
using Rental.API.Vehicles.Providers;
using System;
using System.Linq;
using Xunit;

namespace Rental.API.Vehicles.Tests
{
    public class CarModelProviderTest
    {
        public static IWebHostEnvironment _environment;
        [Fact]
        public async void GetCarModelsReturnAllCarModels()
        {
            //var options = new DbContextOptionsBuilder<VehiclesDBContext>().UseInMemoryDatabase(nameof(GetCarModelsReturnAllCarModels)).Options;
            //var dbContext = new VehiclesDBContext(options);
            //CreateCarModels(dbContext);

            //var carModelProfile = new CarModelProfile();
            //var configuration = new MapperConfiguration(config => config.AddProfile(carModelProfile));
            //var mapper = new Mapper(configuration);
            //var carModelProvider = new CarModelsProvider(dbContext, null, mapper, _environment);

            //var carModels = await carModelProvider.GetCarModelsAsync();

            //Assert.True(carModels.IsSuccess);
            //Assert.True(carModels.CarModels.Any());
            //Assert.Null(carModels.ErrorMessage);
        }

        [Fact]
        public async void GetMakeReturnMakeUsingValidId()
        {
        }

        [Fact]
        public async void GetMakeReturnMakeUsingInvalidId()
        {

        }

        private void CreateCarModels(VehiclesDBContext dbContext)
        {
            dbContext.FuelTypes.Add(new FuelType() { FuelTypeName = Guid.NewGuid().ToString() });
            dbContext.Makes.Add(new Make() { Name = Guid.NewGuid().ToString() });
            dbContext.VehicleCategories.Add(new VehicleCategory() { CategoryName = Guid.NewGuid().ToString() });
            for (int i = 0; i < 10; i++)
            {
                dbContext.CarModels.Add(new CarModel()
                {
                    Name = $"Name{i}",
                    FuelTypeID = 1,
                    MakeID = 1,
                    RentalPricePerHour = 1,
                    VehicleCategoryID = 1,
                    TrunkLimit = 1,
                    ImagePath = "test"
                });
            }
            dbContext.SaveChanges();
        }
    }
}
