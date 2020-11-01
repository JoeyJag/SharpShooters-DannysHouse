namespace Sharpshooter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        MenuItemID = c.Int(nullable: false),
                        ReviewOfItem = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemID)
                .Index(t => t.MenuItemID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "MenuItemID", "dbo.MenuItems");
            DropIndex("dbo.Reviews", new[] { "MenuItemID" });
            DropTable("dbo.Reviews");
        }
    }
}
