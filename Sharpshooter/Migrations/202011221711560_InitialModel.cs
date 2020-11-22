namespace Sharpshooter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "DriverID", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderDetails", "DriverID");
            AddForeignKey("dbo.OrderDetails", "DriverID", "dbo.Drivers", "DriverID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "DriverID", "dbo.Drivers");
            DropIndex("dbo.OrderDetails", new[] { "DriverID" });
            DropColumn("dbo.OrderDetails", "DriverID");
        }
    }
}
