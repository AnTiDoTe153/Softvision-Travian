namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class troups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Troups",
                c => new
                    {
                        TroupId = c.Int(nullable: false, identity: true),
                        TroupTypeId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        TroupCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TroupId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.TroupTypes", t => t.TroupTypeId, cascadeDelete: true)
                .Index(t => t.TroupTypeId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.TroupTypes",
                c => new
                    {
                        TroupTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Attack = c.Double(nullable: false),
                        Defence = c.Double(nullable: false),
                        CreationSpeed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TroupTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Troups", "TroupTypeId", "dbo.TroupTypes");
            DropForeignKey("dbo.Troups", "CityId", "dbo.Cities");
            DropIndex("dbo.Troups", new[] { "CityId" });
            DropIndex("dbo.Troups", new[] { "TroupTypeId" });
            DropTable("dbo.TroupTypes");
            DropTable("dbo.Troups");
        }
    }
}
