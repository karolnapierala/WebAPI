using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }
        public string? Descripton { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        public int PostalCode { get; set; }
    }
}
