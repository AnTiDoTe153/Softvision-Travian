using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace DataAccess
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext dbContext;

        public UserRepository()
        {
            dbContext = new ApplicationDbContext();
        }

        public ApplicationUser GetUser(string userId)
        {
            var user = dbContext.Users.Find(userId);
            return user;
        }
    }
}
