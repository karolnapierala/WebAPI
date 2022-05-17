using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApp.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<CreatedRestaurantsRequierement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedRestaurantsRequierement requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            _logger.LogInformation($"User: {userEmail} with date of birth: [{dateOfBirth}]");

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                _logger.LogInformation("Authorization succedded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed");

            }

            return Task.CompletedTask;
        }
    }
}
