using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Services.Dtos;

namespace ShoppingCart.Services.Services
{
    internal class UserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public UserDto GetUser(int id)
        {
            var user = _dbContext.Users.AsNoTracking().SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new Exception($"User with id {id} not found");
            }

            return new UserDto (user.Id, user.Username );
        }
    }
}
