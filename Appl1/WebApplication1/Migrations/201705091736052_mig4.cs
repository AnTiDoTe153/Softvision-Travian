namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BuildingTypes", "City_CityId", "dbo.Cities");
            DropIndex("dbo.BuildingTypes", new[] { "City_CityId" });
            DropColumn("dbo.BuildingTypes", "City_CityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BuildingTypes", "City_CityId", c => c.Int());
            CreateIndex("dbo.BuildingTypes", "City_CityId");
            AddForeignKey("dbo.BuildingTypes", "City_CityId", "dbo.Cities", "CityId");
        }
    }
}
