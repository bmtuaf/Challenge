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
            var options = new DbContextOptionsBuilder<VehiclesDBContext>().UseInMemoryDatabase(nameof(GetCarModelsReturnAllCarModels)).Options;
            var dbContext = new VehiclesDBContext(options);
            CreateCarModels(dbContext);

            var carModelProfile = new CarModelProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(carModelProfile));
            var mapper = new Mapper(configuration);
            var carModelProvider = new CarModelsProvider(dbContext, null, mapper, _environment);

            var carModels = await carModelProvider.GetCarModelsAsync();

            Assert.True(carModels.IsSuccess);
            Assert.True(carModels.CarModels.Any());
            Assert.Null(carModels.ErrorMessage);
        }

        //[Fact]
        //public async void GetMakeReturnMakeUsingValidId()
        //{
        //    var options = new DbContextOptionsBuilder<VehiclesDBContext>().UseInMemoryDatabase(nameof(GetMakeReturnMakeUsingValidId)).Options;
        //    var dbContext = new VehiclesDBContext(options);
        //    CreateMakes(dbContext);

        //    var makeProfile = new MakeProfile();
        //    var configuration = new MapperConfiguration(config => config.AddProfile(makeProfile));
        //    var mapper = new Mapper(configuration);
        //    var makesProvider = new MakesProvider(dbContext, null, mapper);

        //    var make = await makesProvider.GetMakeAsync(1);

        //    Assert.True(make.IsSuccess);
        //    Assert.NotNull(make.Make);
        //    Assert.True(make.Make.ID == 1);
        //    Assert.Null(make.ErrorMessage);
        //}

        //[Fact]
        //public async void GetMakeReturnMakeUsingInvalidId()
        //{
        //    var options = new DbContextOptionsBuilder<VehiclesDBContext>().UseInMemoryDatabase(nameof(GetMakeReturnMakeUsingValidId)).Options;
        //    var dbContext = new VehiclesDBContext(options);
        //    CreateMakes(dbContext);

        //    var makeProfile = new MakeProfile();
        //    var configuration = new MapperConfiguration(config => config.AddProfile(makeProfile));
        //    var mapper = new Mapper(configuration);
        //    var makesProvider = new MakesProvider(dbContext, null, mapper);

        //    var make = await makesProvider.GetMakeAsync(-1);

        //    Assert.False(make.IsSuccess);
        //    Assert.Null(make.Make);
        //    Assert.NotNull(make.ErrorMessage);
        //}

        private void CreateCarModels(VehiclesDBContext dbContext)
        {
            dbContext.FuelTypes.Add(new FuelType() { FuelTypeName = Guid.NewGuid().ToString() });
            dbContext.Makes.Add(new Make() { Name = Guid.NewGuid().ToString() });
            dbContext.VehicleCategories.Add(new VehicleCategory() { CategoryName = Guid.NewGuid().ToString() });
            for (int i = 0; i < 10; i++)
            {
                dbContext.CarModels.Add(new CarModel()
                {
                    Name = Guid.NewGuid().ToString(),
                    FuelTypeID = 1,
                    MakeID = 1,
                    RentalPricePerHour = 1,
                    VehicleCategoryID = 1,
                    TrunkLimit = 1,
                    ImagePath = string.Empty
                });
            }
            dbContext.SaveChanges();
        }
    }
}
