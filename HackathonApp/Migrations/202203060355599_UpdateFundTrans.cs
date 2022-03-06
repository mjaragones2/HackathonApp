namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFundTrans : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundTransactions", "ReceiverId", c => c.String(maxLength: 128));
            CreateIndex("dbo.FundTransactions", "ReceiverId");
            AddForeignKey("dbo.FundTransactions", "ReceiverId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FundTransactions", "ReceiverId", "dbo.AspNetUsers");
            DropIndex("dbo.FundTransactions", new[] { "ReceiverId" });
            DropColumn("dbo.FundTransactions", "ReceiverId");
        }
    }
}
