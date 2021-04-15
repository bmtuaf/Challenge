using Microsoft.AspNetCore.Mvc;
using Rental.API.Orchestrator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rental.API.Orchestrator.Models.RequestModels;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Rental.API.Orchestrator.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    public class ReservationOrchestratorController : ControllerBase
    {
        private readonly IReservationOrchestratorService reservationOrchestratorService;

        public ReservationOrchestratorController(IReservationOrchestratorService reservationOrchestratorService)
        {
            this.reservationOrchestratorService = reservationOrchestratorService;
        }


        [HttpPost]        
        public async Task<IActionResult> PostReservationAsync(ReservationRequest reservation)
        {
            var result = await reservationOrchestratorService.PostReservationAsync(reservation);
            if (result.IsSuccess)
            {
                MemoryStream workStream = new MemoryStream();
                Document document = new Document();
                PdfWriter.GetInstance(document, workStream).CloseStream = false;

                document.Open();
                document.Add(new Paragraph(result.RentalContract));
                document.Add(new Paragraph(DateTime.Now.ToString()));
                document.Close();

                byte[] byteInfo = workStream.ToArray();
                workStream.Write(byteInfo, 0, byteInfo.Length);
                workStream.Position = 0;

                return new FileStreamResult(workStream, "application/pdf");
            }
            return NotFound();
        }

    }
}
