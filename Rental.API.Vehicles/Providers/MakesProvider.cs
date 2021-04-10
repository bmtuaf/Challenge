﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rental.API.Vehicles.DB;
using Rental.API.Vehicles.Interfaces;
using Rental.API.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.API.Vehicles.Providers
{
    public class MakesProvider : IMakesProvider
    {
        private readonly VehiclesDBContext dBContext;
        private readonly ILogger<MakesProvider> logger;
        private readonly IMapper mapper;

        public MakesProvider(VehiclesDBContext dBContext, ILogger<MakesProvider> logger, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dBContext.Makes.Any())
            {
                dBContext.Makes.Add(new DB.Make() { ID = 1, Name = "Audi" });
                dBContext.Makes.Add(new DB.Make() { ID = 2, Name = "BMW" });
                dBContext.Makes.Add(new DB.Make() { ID = 3, Name = "Chevrolet" });
                dBContext.Makes.Add(new DB.Make() { ID = 4, Name = "Fiat" });
                dBContext.Makes.Add(new DB.Make() { ID = 5, Name = "Ford" });
                dBContext.Makes.Add(new DB.Make() { ID = 6, Name = "Volkswagen" });
                dBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Make> Makes, string ErrorMessage)> GetMakesAsync()
        {
            try
            {
                var makes = await dBContext.Makes.ToListAsync();
                if (makes != null && makes.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Make>, IEnumerable<Models.Make>>(makes);
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

        public async Task<(bool IsSuccess, Models.Make Make, string ErrorMessage)> GetMakeAsync(int id)
        {
            try
            {
                var make = await dBContext.Makes.FirstOrDefaultAsync(m => m.ID == id);

                if (make != null)
                {
                    var result = mapper.Map<DB.Make, Models.Make>(make);
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

        public async Task<(bool IsSuccess, Models.Make Make, string ErrorMessage)> PostMakeAsync(string name)
        {
            try
            {
                var make = new DB.Make() { Name = name };
                dBContext.Add(make);                
                if (await dBContext.SaveChangesAsync() > 0)
                {
                    var result = mapper.Map<DB.Make, Models.Make>(make);
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

        public async Task<(bool IsSuccess, Models.Make Make, string ErrorMessage)> DeleteMakeAsync(int id)
        {
            try
            {
                var make = new DB.Make() { ID = id };
                dBContext.Remove(make);
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

        public async Task<(bool IsSuccess, Models.Make Make, string ErrorMessage)> PutMakeAsync(Models.Make make)
        {
            try
            {
                var entity = await dBContext.Makes.FirstOrDefaultAsync(m => m.ID == make.ID);
                if (entity != null)
                {
                    entity.Name = make.Name;
                    dBContext.Update(entity);
                    if (await dBContext.SaveChangesAsync() > 0)
                    {
                        var result = mapper.Map<DB.Make, Models.Make>(entity);
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