using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UpdateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }
        public string? Descripton { get; set; }
        public bool HasDelivery { get; set; }
    }
}
