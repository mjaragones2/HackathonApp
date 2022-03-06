namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funds", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Funds", "Status");
        }
    }
}
