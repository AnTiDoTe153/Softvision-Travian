namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Buildings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuildingTypes",
                c => new
                    {
                        BuildingTypeId = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        City_CityId = c.Int(),
                    })
                .PrimaryKey(t => t.BuildingTypeId)
                .ForeignKey("dbo.Cities", t => t.City_CityId)
                .Index(t => t.City_CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuildingTypes", "City_CityId", "dbo.Cities");
            DropIndex("dbo.BuildingTypes", new[] { "City_CityId" });
            DropTable("dbo.BuildingTypes");
        }
    }
}
