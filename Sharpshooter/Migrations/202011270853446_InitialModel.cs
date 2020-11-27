namespace Sharpshooter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PayAtStore", c => c.Boolean(nullable: false));
            DropColumn("dbo.OrderDetails", "PayAtStore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "PayAtStore", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "PayAtStore");
        }
    }
}
