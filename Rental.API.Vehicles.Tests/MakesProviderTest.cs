using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rental.API.Vehicles.DB;
using Rental.API.Vehicles.Profiles;
using Rental.API.Vehicles.Providers;
using System;
using System.Linq;
using Xunit;

namespace Rental.API.Vehicles.Tests
{
    public class MakesProviderTest
    {
        [Fact]
        public async void GetMakesReturnAllMakes()
        {
            var options = new DbContextOptionsBuilder<VehiclesDBContext>().UseInMemoryDatabase(nameof(GetMakesReturnAllMakes)).Options;
            var dbContext = new VehiclesDBContext(options);
            CreateMakes(dbContext);

            var makeProfile = new MakeProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(makeProfile));
            var mapper = new Mapper(configuration);
            var makesProvider = new MakesProvider(dbContext,null, mapper);

            var makes = await makesProvider.GetMakesAsync();

            Assert.True(makes.IsSuccess);
            Assert.True(makes.Makes.Any());
            Assert.Null(makes.ErrorMessage);
        }

        [Fact]
        public async void GetMakeReturnMakeUsingValidId()
        {
            var options = new DbContextOptionsBuilder<VehiclesDBContext>().UseInMemoryDatabase(nameof(GetMakeReturnMakeUsingValidId)).Options;
            var dbContext = new VehiclesDBContext(options);
            CreateMakes(dbContext);

            var makeProfile = new MakeProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(makeProfile));
            var mapper = new Mapper(configuration);
            var makesProvider = new MakesProvider(dbContext, null, mapper);

            var make = await makesProvider.GetMakeAsync(1);

            Assert.True(make.IsSuccess);
            Assert.NotNull(make.Make);
            Assert.True(make.Make.ID == 1);
            Assert.Null(make.ErrorMessage);
        }

        [Fact]
        public async void GetMakeReturnMakeUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<VehiclesDBContext>().UseInMemoryDatabase(nameof(GetMakeReturnMakeUsingValidId)).Options;
            var dbContext = new VehiclesDBContext(options);
            CreateMakes(dbContext);

            var makeProfile = new MakeProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(makeProfile));
            var mapper = new Mapper(configuration);
            var makesProvider = new MakesProvider(dbContext, null, mapper);

            var make = await makesProvider.GetMakeAsync(-1);

            Assert.False(make.IsSuccess);
            Assert.Null(make.Make);
            Assert.NotNull(make.ErrorMessage);
        }

        [Fact]
        public async void PostMakeAsyncValidName()
        {

        }
        [Fact]
        public async void PostMakeAsyncInvalidName()
        {

        }
        [Fact]
        public async void DeleteMakeAsyncValidId()
        {

        }
        [Fact]
        public async void DeleteMakeAsyncInvalidId()
        {

        }
        [Fact]
        public async void PutMakeAsyncValidId()
        {

        }
        [Fact]
        public async void PutMakeAsyncInvalidId()
        {

        }

        private void CreateMakes(VehiclesDBContext dbContext)
        {
            for (int i = 0; i < 10; i++)
            {
                dbContext.Makes.Add(new Make()
                {                    
                    Name = Guid.NewGuid().ToString()
                });                
            }
            dbContext.SaveChanges();
        }
    }
}
