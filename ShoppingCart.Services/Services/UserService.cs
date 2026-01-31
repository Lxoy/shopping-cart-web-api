using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data;
using ShoppingCart.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Services.Services
{
    internal class UserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public UserDTO GetUser(int id)
        {
            var user = _dbContext.Users.AsNoTracking().SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new Exception($"User with id {id} not found");
            }

            return new UserDTO { Id = user.Id, Username = user.Username };
        }
    }
}
