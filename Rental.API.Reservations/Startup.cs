using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rental.API.Reservations.DB;
using Rental.API.Reservations.Interfaces;
using Rental.API.Reservations.Providers;
using Rental.API.Reservations.Utils;

namespace Rental.API.Reservations
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

            services.AddScoped<IReservationsProvider, ReservationsProvider>();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<ReservationsDbContext>(options =>
            {
                options.UseInMemoryDatabase("Reservations");
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
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
                var db = scopedServices.GetRequiredService<ReservationsDbContext>();

                db.Database.EnsureCreated();

                DBUtils.SeedData(db);
            }
        }
    }
}
