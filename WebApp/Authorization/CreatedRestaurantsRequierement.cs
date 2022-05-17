using Microsoft.AspNetCore.Authorization;

namespace WebApp.Authorization
{
    public class CreatedMultipleRestaurantsRequierement : IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; }
        public CreatedMultipleRestaurantsRequierement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }
    }
}
