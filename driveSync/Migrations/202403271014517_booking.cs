namespace driveSync.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        PassengerId = c.Int(nullable: false),
                        RideId = c.Int(nullable: false),
                        InventoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .ForeignKey("dbo.Passengers", t => t.PassengerId, cascadeDelete: true)
                .ForeignKey("dbo.Rides", t => t.RideId, cascadeDelete: true)
                .Index(t => t.PassengerId)
                .Index(t => t.RideId)
                .Index(t => t.InventoryId);
            
            AddColumn("dbo.Inventories", "Booking_BookingId", c => c.Int());
            CreateIndex("dbo.Inventories", "Booking_BookingId");
            AddForeignKey("dbo.Inventories", "Booking_BookingId", "dbo.Bookings", "BookingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "RideId", "dbo.Rides");
            DropForeignKey("dbo.Bookings", "PassengerId", "dbo.Passengers");
            DropForeignKey("dbo.Bookings", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.Inventories", "Booking_BookingId", "dbo.Bookings");
            DropIndex("dbo.Inventories", new[] { "Booking_BookingId" });
            DropIndex("dbo.Bookings", new[] { "InventoryId" });
            DropIndex("dbo.Bookings", new[] { "RideId" });
            DropIndex("dbo.Bookings", new[] { "PassengerId" });
            DropColumn("dbo.Inventories", "Booking_BookingId");
            DropTable("dbo.Bookings");
        }
    }
}
