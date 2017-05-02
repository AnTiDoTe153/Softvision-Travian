using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class MineController : Controller
    {
        // GET: Mine
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public ActionResult Index(int? cityId)
        {
   
            var userId = this.User.Identity.GetUserId();
            var user = dbContext.Users.Find(userId);
<<<<<<< HEAD
            var city = user.Cities.First();

            if (cityId != null)
            {
                city = dbContext.Cities.Find(cityId);
            }
            UpdteResources(city);            
 
            dbContext.SaveChanges();

            return View(city);
        }

        private void UpdteResources(City city)
        {
            var start = DateTime.Now;
            foreach (var res in city.Resources)
            {
                foreach (var mine in city.Mines)
                {
                    if (mine.Type == res.Type)
                    {
                        res.Value += mine.GetProductionPerHour() * (start - res.LastUpdate).TotalHours;
                    }
                }
                res.LastUpdate = start;
            }
=======
            foreach (var city in user.Cities)
            {
                this.UpdteResources(city);
            }
            
 
            user.UpdateCities();
>>>>>>> 4fd3036b03526e54e6b2e5a282e28773724b22c3
            dbContext.SaveChanges();

        }

        private void UpdteResources(City city)
        {
            var start = DateTime.Now;
            foreach (var res in city.Resources)
            {
                foreach (var mine in city.Mines)
                {
                    if (mine.Type == res.Type)
                    {
                        res.Value += mine.GetProductionPerHour() * (start - res.LastUpdate).TotalHours;
                    }
                }
                res.LastUpdate = start;
            }
            dbContext.SaveChanges();

        }

        [HttpPost]
        public ActionResult Index(int mineId)
        {
            var mine = dbContext.Mines.Find(mineId);
            mine.Upgrade();
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Mine");
        }

        [HttpPost]
        public ActionResult AddCity()
        {
            var userId = this.User.Identity.GetUserId();
            var user = dbContext.Users.Find(userId);

            user.Cities.Add(new City
            {
                Mines = new List<Mine>
                {
                    new Mine
                    {
                        Level = 0,
                        Type = ResourceType.Clay,
                    },
                    new Mine
                    {
                        Level = 0,
                        Type = ResourceType.Iron,
                    },
                    new Mine
                    {
                        Level = 0,
                        Type = ResourceType.Wheat,
                    },
                    new Mine
                    {
                        Level = 0,
                        Type = ResourceType.Wood,
                    },
                },
                Resources = new List<Resource>
                {
                    new Resource
                    {
                        Type = ResourceType.Clay,
                        LastUpdate = DateTime.Now,
                    },
                    new Resource
                    {
                        Type = ResourceType.Iron,
                        LastUpdate = DateTime.Now,
                    },
                    new Resource
                    {
                        Type = ResourceType.Wheat,
                        LastUpdate = DateTime.Now,
                    },
                    new Resource
                    {
                        Type = ResourceType.Wood,
                        LastUpdate = DateTime.Now,
                    },
                }
            });
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Mine");
        }

        public ActionResult Details(int mineId)
        {
            var mine = dbContext.Mines.Find(mineId);
            
            return View(mine);
        }
    }
}