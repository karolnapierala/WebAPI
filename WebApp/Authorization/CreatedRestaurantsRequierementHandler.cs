using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApp.Entities;

namespace WebApp.Authorization
{
    public class CreatedMultipleRestaurantsRequierementHandler : AuthorizationHandler<CreatedMultipleRestaurantsRequierement>
    {
        private readonly RestaurantDbContext _context;
        public CreatedMultipleRestaurantsRequierementHandler(RestaurantDbContext context)
        {
            _context = context;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequierement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdRestaurantsCount = _context.Restaurants.Count(r => r.CreatedById == userId);
            if (createdRestaurantsCount > requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
