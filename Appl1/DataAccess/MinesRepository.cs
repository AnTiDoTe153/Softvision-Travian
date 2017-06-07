using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace DataAccess
{
    public class MinesRepository : IMinesRepository
    {
        private ApplicationDbContext dbContext;

        public MinesRepository()
        {
            dbContext = new ApplicationDbContext();
        }

        public City getCity(ApplicationUser user)
        {
            var city = user.Cities.First();
            return city;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }

}
