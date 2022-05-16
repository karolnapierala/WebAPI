using WebApp.Entities;

namespace WebApp
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();

                }
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                Name = "User"
                },
                new Role()
                {
                Name = "Manager"
                },
                new Role()
                {
                Name = "Admin"
                }
            };
            return roles;
    }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Descripton = "shgkfsdgisdfughdfiugh h uisaogiuoagag riugisgosg",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "41423545",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Hot Chicken",
                            Price = 10.30M,
                        },
                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 6.30M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = 30001
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald's",
                    Category = "Fast Food",
                    Descripton = "shgkfsdgisdfughdfiugh h uisaogiuoagag riugisgosg",
                    ContactEmail = "contact@mcdonald.com",
                    ContactNumber = "41423545",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Cheeseburger",
                            Price = 5.40M,
                        },
                        new Dish()
                        {
                            Name = "Nuggets",
                            Price = 8.30M,
                        },
                        new Dish()
                        {
                            Name = "Hamburger",
                            Price = 4.80M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Poznań",
                        Street = "Krótka 5",
                        PostalCode = 67410
                    }
                },
            };
            return restaurants;
        }
    }
}
