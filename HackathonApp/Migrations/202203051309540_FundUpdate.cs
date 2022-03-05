namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FundUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funds", "Userid", c => c.String(maxLength: 128));
            CreateIndex("dbo.Funds", "Userid");
            AddForeignKey("dbo.Funds", "Userid", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Funds", "Userid", "dbo.AspNetUsers");
            DropIndex("dbo.Funds", new[] { "Userid" });
            DropColumn("dbo.Funds", "Userid");
        }
    }
}
