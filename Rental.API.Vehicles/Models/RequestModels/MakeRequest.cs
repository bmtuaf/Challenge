using System.ComponentModel.DataAnnotations;

namespace Rental.API.Vehicles.Models.RequestModels
{
    public class MakeRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
