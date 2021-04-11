using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rental.API.Vehicles.DB;
using Rental.API.Vehicles.Interfaces;
using Rental.API.Vehicles.Providers;
using Rental.API.Vehicles.Utils;

namespace Rental.API.Vehicles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMakesProvider, MakesProvider>();
            services.AddScoped<ICarModelsProvider, CarModelsProvider>();
            services.AddScoped<IVehicleCategoriesProvider, VehicleCategoriesProvider>();
            services.AddScoped<IFuelTypesProvider, FuelTypesProvider>();
            services.AddScoped<IVehiclesProvider, VehiclesProvider>();

            services.AddAutoMapper(typeof(Startup));   
            
            services.AddDbContext<VehiclesDBContext>(options =>
            {
                options.UseInMemoryDatabase("Vehicles");
            }); 

            services.AddControllers();
            services.AddSwaggerGen();

            InitializeDB(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","My API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InitializeDB(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<VehiclesDBContext>();

                db.Database.EnsureCreated();

                DBUtils.SeedData(db);
            }
        }
    }
}
