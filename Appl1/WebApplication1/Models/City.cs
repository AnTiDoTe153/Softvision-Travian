﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public virtual IList<Troup> Troups { get; set; }

        public int MaxWheat
        {
            get
            {
                int sum = 0;
                foreach (var b in Buildings)
                {
                    if (b.BuildingType?.Name == "Granary")
                    {
                        sum += 100 + 100 * b.Level;
                    }
                }
                return sum;
            }
        }

        public int MaxRes
        {
            get
            {
                int sum = 0;
                foreach (var b in Buildings)
                {
                    if (b.BuildingType?.Name == "Barn")
                    {
                        sum += 100 + 100 * b.Level;
                    }
                }
                return sum;
            }
        }


        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

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
        public string BuildingStyle { get; set; }
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
        public DateTime UpgradeCompletesAt { get; set; }
        public bool IsUpgrading { get { return this.UpgradeCompletesAt > DateTime.Now; } }

        public int Level { get; set; }

        public ResourceType Type { get; set; }

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
    public class Troup
    {
        public int TroupId { get; set; }
        public int TroupTypeId { get; set; }
        public virtual TroupType TroupType { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int TroupCount { get; set; }

    }

    public class TroupType
    {
        public int TroupTypeId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        [RegularExpression("[A-z]*")]
        public string Name { get; set; }



        [Required]
        [Range(0, 100)]
        public double Attack { get; set; }

        [Required]
        [Range(0, 100)]
        public double Defence { get; set; }

        [Required]
        [Range(0, 100)]
        public int CreationSpeed { get; set; }
        
    }

    public class CityFilterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [DisplayName("Min Troup Count")]
        public int? MinTroupCount { get; set; }
        [DisplayName("Max Troup Count")]
        public int? MaxTroupCount { get; set; }

        public List<City> Results { get; set; }
    }

    public enum ResourceType
    {
        Wheat,
        Iron,
        Clay,
        Wood
    }
}