namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingId = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        BuildingTypeId = c.Int(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BuildingId)
                .ForeignKey("dbo.BuildingTypes", t => t.BuildingTypeId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.BuildingTypeId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buildings", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Buildings", "BuildingTypeId", "dbo.BuildingTypes");
            DropIndex("dbo.Buildings", new[] { "CityId" });
            DropIndex("dbo.Buildings", new[] { "BuildingTypeId" });
            DropTable("dbo.Buildings");
        }
    }
}
