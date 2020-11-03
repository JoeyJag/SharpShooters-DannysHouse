namespace Sharpshooter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantReviews",
                c => new
                    {
                        RestaurantReviewID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        ReviewOfRestraurant = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RestaurantReviewID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RestaurantReviews");
        }
    }
}
