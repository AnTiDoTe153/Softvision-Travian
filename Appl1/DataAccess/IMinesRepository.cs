using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace DataAccess
{
    public interface IMinesRepository
    {
        City getCity(ApplicationUser user);
        void SaveChanges();
    }
}
