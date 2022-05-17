

using Microsoft.AspNetCore.Authorization;

namespace WebApp.Authorization
{
    public class CreatedRestaurantsRequierement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }

        public CreatedRestaurantsRequierement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
