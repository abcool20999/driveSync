namespace driveSync.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ride : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rides",
                c => new
                    {
                        RideId = c.Int(nullable: false, identity: true),
                        startLocation = c.String(),
                        endLocation = c.String(),
                        price = c.String(),
                        Time = c.DateTime(nullable: false),
                        dayOftheweek = c.String(),
                        DriverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RideId)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rides", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Rides", new[] { "DriverId" });
            DropTable("dbo.Rides");
        }
    }
}
