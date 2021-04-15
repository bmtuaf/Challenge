using Microsoft.Extensions.Logging;
using Rental.API.Orchestrator.Interfaces;
using Rental.API.Orchestrator.Models.RequestModels;
using Rental.API.Orchestrator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rental.API.Orchestrator.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ReservationsService> logger;

        public VehiclesService(IHttpClientFactory httpClientFactory, ILogger<ReservationsService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<AvailableCarModels> AvailableCarModels, string ErrorMessage)> GetAvailableCarModelsAsync(List<int> search)
        {
            try
            {
                var requestContent = JsonSerializer.Serialize(search);
                var buffer = System.Text.Encoding.UTF8.GetBytes(requestContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var client = httpClientFactory.CreateClient("VehiclesService");
                var response = await client.PostAsync($"api/carmodels/availablemodels", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<List<AvailableCarModels>>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);

            }
        }

        public async Task<(bool IsSuccess, Vehicle Vehicle, string ErrorMessage)> PostReservationVehicleAsync(VehicleReservationRequest reservationRequest)
        {
            try
            {
                var requestContent = JsonSerializer.Serialize(reservationRequest);
                var buffer = System.Text.Encoding.UTF8.GetBytes(requestContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var client = httpClientFactory.CreateClient("VehiclesService");
                var response = await client.PostAsync($"api/vehicles/reservationvehicle", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Vehicle>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);

            }
        }
    }
}
