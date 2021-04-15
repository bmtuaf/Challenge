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
    public class ReservationsService : IReservationsService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ReservationsService> logger;

        public ReservationsService(IHttpClientFactory httpClientFactory, ILogger<ReservationsService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool IsSuccess, NotAvailableVehicles VehiclesNotAvailable, string ErrorMessage)> GetNotAvailableVehiclesAsync(SearchVehicleAvailability search)
        {
            try
            {
                var requestContent = JsonSerializer.Serialize(search);
                var buffer = System.Text.Encoding.UTF8.GetBytes(requestContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var client = httpClientFactory.CreateClient("ReservationsService");
                var response = await client.PostAsync($"api/reservations/notavailablevehicles", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<NotAvailableVehicles>(content, options);
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

        public async Task<(bool IsSuccess, IEnumerable<Reservation> Reservations, string ErrorMessage)> GetUsersHistoricalReservationsAsync(SearchUserReservation search)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ReservationsService");
                var response = await client.GetAsync($"api/reservations/user/historical/{search.CPF}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Reservation>>(content, options);
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

        public async Task<(bool IsSuccess, IEnumerable<Reservation> Reservations, string ErrorMessage)> GetUsersReservationsAsync(SearchUserReservation search)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ReservationsService");
                var response = await client.GetAsync($"api/reservations/user/active/{search.CPF}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Reservation>>(content, options);
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

        public async Task<(bool IsSuccess, Reservation Reservation, string ErrorMessage)> PostReservationAsync(ReservationPersistRequest reservation)
        {
            try
            {
                var requestContent = JsonSerializer.Serialize(reservation);
                var buffer = System.Text.Encoding.UTF8.GetBytes(requestContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var client = httpClientFactory.CreateClient("ReservationsService");
                var response = await client.PostAsync($"api/reservations", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Reservation>(content, options);
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
