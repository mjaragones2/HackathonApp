namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funds", "Counter", c => c.Int(nullable: false));
            DropColumn("dbo.LikeReacts", "Counter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LikeReacts", "Counter", c => c.Int(nullable: false));
            DropColumn("dbo.Funds", "Counter");
        }
    }
}
