using System.ComponentModel.DataAnnotations;

namespace Rental.API.Vehicles.Models.RequestModels
{
    public class MakeUpdateRequest
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
