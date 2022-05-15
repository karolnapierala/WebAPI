namespace WebApp.Models
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descripton { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public List<DishDto> Dishes { get; set; }
    }
}
