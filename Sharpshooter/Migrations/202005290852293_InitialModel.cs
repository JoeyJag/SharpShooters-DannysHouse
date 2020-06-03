namespace Sharpshooter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "MenuImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "MenuImg");
        }
    }
}
