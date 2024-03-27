namespace driveSync.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passenger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        PassengerId = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        username = c.String(),
                        password = c.String(),
                        lastName = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.PassengerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Passengers");
        }
    }
}
