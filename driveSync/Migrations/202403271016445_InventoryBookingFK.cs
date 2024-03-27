namespace driveSync.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryBookingFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "BookingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Inventories", "BookingId");
            AddForeignKey("dbo.Inventories", "BookingId", "dbo.Bookings", "BookingId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "BookingId", "dbo.Bookings");
            DropIndex("dbo.Inventories", new[] { "BookingId" });
            DropColumn("dbo.Inventories", "BookingId");
        }
    }
}
