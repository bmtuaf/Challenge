using System.ComponentModel.DataAnnotations;

namespace Rental.API.Orchestrator.Models.RequestModels
{
    public class SearchUserReservation
    {
        [Required]
        public string CPF { get; set; }
    }
}
