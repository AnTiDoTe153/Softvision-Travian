using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CitiesController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        // GET: Cities
        public ActionResult Index(CityFilterViewModel cityFilter)

        {

            IQueryable<City> query = dbContext.Cities;
            cityFilter.Results = dbContext.Cities.ToList();
            if (cityFilter.Name != null)
            {
                query = query.Where(u => u.ApplicationUser.UserName.Contains(cityFilter.Name));
            }
            if (cityFilter.Email != null)
            {
                query = query.Where(u => u.ApplicationUser.UserName.Contains(cityFilter.Email));
            }
            if (cityFilter.MinTroupCount != null)
            {
                query = query.Where(u => u.Troups.Sum(x => x.TroupCount) > cityFilter.MinTroupCount);
            }
            if (cityFilter.MaxTroupCount != null)
            {
                query = query.Where(u => u.Troups.Sum(x => x.TroupCount) < cityFilter.MaxTroupCount);
            }
            cityFilter.Results = query.ToList();
   


            return View(cityFilter);
        }
    }
}