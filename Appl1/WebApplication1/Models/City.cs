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
        public virtual IList<Building> Buildings { get; set; }

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

    public class Building
    {
        public int BuildingId { get; set; }
        public int Level { get; set; }
        public int? BuildingTypeId { get; set; }
        public virtual BuildingType BuildingType { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }

    public class BuildingType
    {
        public int BuildingTypeId { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }


    public class Mine
    {
        public DateTime UpgradeCompletion { get; set; }
        public int MineId { get; set; }
        public virtual City City { get; set; }
        public string MineStyle { get; set; }
        public DateTime UpgradeCompetesAt { get; set; }
        public bool IsUpgrading { get { return this.UpgradeCompetesAt > DateTime.Now; } }

        public int Level { get; set; }


        public ResourceType Type { get; set; }

        public void Upgrade()
        {
            if (UpgradeCompletion <= DateTime.Now)
            {
                Level++;
                UpgradeCompletion = DateTime.Now.AddHours(1);
            }  
        }

        internal (int amount, ResourceType type)[] GetUpgradeRequirements()
        {
            return new[]
            {
                (10 * (this.Level + 1), ResourceType.Clay),
                (10 * (this.Level + 1), ResourceType.Wheat),
                (10 * (this.Level + 1), ResourceType.Wood),
                (10 * (this.Level + 1), ResourceType.Iron),
            };
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