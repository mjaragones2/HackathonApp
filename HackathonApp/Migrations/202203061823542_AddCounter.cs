namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LikeReacts", "Counter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LikeReacts", "Counter");
        }
    }
}
