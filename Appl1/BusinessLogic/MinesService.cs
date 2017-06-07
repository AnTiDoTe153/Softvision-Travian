using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace BusinessLogic
{
    public class MinesService : IMinesService
    {
        private IUserRepository userRepository;
        private IMinesRepository minesRepository;

        public MinesService()
        {
            userRepository = new UserRepository();
            minesRepository = new MinesRepository();
        }

        public City UpdateResources(string userId)
        {
            var user = userRepository.GetUser(userId);
            var city = minesRepository.getCity(user);

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
            return city;
        }
    }
}
