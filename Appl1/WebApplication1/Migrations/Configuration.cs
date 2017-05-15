namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebApplication1.Models.ApplicationDbContext";
        }

        protected override void Seed(WebApplication1.Models.ApplicationDbContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.BuildingTypes.AddOrUpdate(
                        p => p.Name,
                        new Models.BuildingType{ Name = "Granary"},
                        new Models.BuildingType { Name = "Barn" },
                        new Models.BuildingType { Name = "Barracks" }
                    );

            var cities = context.Cities.ToList();
            
            foreach(var city in cities)
            {
                
                for(int i = 0; i < 10; i++)
                {
                    var building = city.Buildings.ElementAtOrDefault(i);
                    
                    if (building == null)
                    {
                        building = new Building { City = city };
                        city.Buildings.Add(building);
                    }
                }
            }
        }
    }
}
