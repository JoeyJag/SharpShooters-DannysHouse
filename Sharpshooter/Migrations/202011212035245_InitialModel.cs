namespace Sharpshooter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "DeliveryGuy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "DeliveryGuy");
        }
    }
}
