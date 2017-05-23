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
            var city = user.Cities.First();

            if (cityId != null)
            {
                city = dbContext.Cities.Find(cityId);
            }
            UpdateResources(city);            
 
            dbContext.SaveChanges();

            return View(city);
        }

        private void UpdateResources(City city)
        {
            var start = DateTime.Now;
            foreach (var res in city.Resources)
            {
                foreach (var mine in city.Mines)
                {
                    if (mine.IsUpgrading == false)
                    {
                        if (mine.Type == res.Type)
                        {
                            res.Value += mine.GetProductionPerHour() * (start - res.LastUpdate).TotalHours;

                        }

                        if (res.Type == ResourceType.Wheat)
                        {
                            if (res.Value > city.MaxWheat)
                            {
                                res.Value = city.MaxWheat;
                            }
                        }
                        else
                        {
                            if (res.Value > city.MaxRes)
                            {
                                res.Value = city.MaxRes;
                            }
                        }
                    }
                }
                res.LastUpdate = start;
            }
            dbContext.SaveChanges();

        }
        
        [HttpPost]
        public ActionResult Upgrade(int mineId, bool fastUpgrade)
        {
            var mine = this.dbContext.Mines.Find(mineId);
            var city = mine.City;
            var needed = mine.GetUpgradeRequirements();
            var have = city.Resources;

            if (fastUpgrade)
            {
                needed = needed.Select(n => (amount: n.amount * 10, type: n.type)).ToArray();
            }

            var r = needed.Join(have, n => n.type, h => h.Type, (n, h)=> (needed:n, have:h));

            if(r.All(_=>_.needed.amount > _.have.Value))
            {
                return View(new MessageViewModel
                {
                    Message = $"You do not have enough resources."
                }
                );

            }
            if (mine.IsUpgrading == true)
            {
                return View(new MessageViewModel
                    {
                        Message = $"Mine is still upgrading."
                    }
                );
            }

            foreach(var item in r)
            {
                item.have.Value -= item.needed.amount;
            }
            mine.Level++;
            mine.UpgradeCompletesAt = DateTime.Now.AddHours(0.5 * mine.Level);
            this.dbContext.SaveChanges();

            return View(new MessageViewModel
            {
                Message = $"Mine {mineId} has been succesfully upgraded."
            }
                );
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
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-clay-1"

                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Clay,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-clay-2"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Clay,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-clay-3"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Iron,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-iron-1"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Iron,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-iron-2"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Wheat,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-wheat-1"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Wheat,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-wheat-2"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Wheat,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-wheat-3"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Wheat,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-wheat-4"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Wheat,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-wheat-5"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Wheat,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-wheat-6"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Wood,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-wood-1"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Wood,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-wood-2"
                               },
                               new Mine
                               {
                                   Level = 0,
                                   Type = ResourceType.Wood,
                                   UpgradeCompletion = DateTime.Now,
                                   MineStyle = "mine-wood-3"
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



        public ActionResult Details(int id)
        {
            var mine = dbContext.Mines.Find(id);

            return View(mine);
        }

    }
}