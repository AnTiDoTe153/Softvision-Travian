using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class City
    {
        public int CityId { get; set; }
        public virtual IList<Mine> Mines { get; set; }
        public virtual IList<Resource> Resources { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public void Update()
        {
            foreach (Resource res in Resources)
            {
                res.Update();
            }
        }

    }

    public class Resource
    {

        public int ResourceId { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public DateTime LastUpdate { get; set; }
        public ResourceType Type { get; set; }

        public void Update()
        {
            double hours = (DateTime.Now - LastUpdate).TotalHours;
            foreach (var mine in City.Mines)
            {
                if (mine.Type == Type)
                {
                    Value += hours * 13 * mine.Level;
                }
            }
            LastUpdate = DateTime.Now;
        }

        public double Value { get; set; }
    }

    public class Mine
    {
        public int MineId { get; set; }
        public virtual City City { get; set; }

        public int Level { get; set; }


        public ResourceType Type { get; set; }

        public void Upgrade()
        {
            Level++;
        }

        public double GetProductionPerHour(int? level = null)
        {
            return (level ?? this.Level) * 13;
        }
    }

    public enum ResourceType
    {
        Wheat,
        Iron,
        Clay,
        Wood
    }
}