namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFundTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Userid = c.String(maxLength: 128),
                        Fundid = c.Int(nullable: false),
                        AmountGiven = c.Decimal(precision: 18, scale: 2),
                        Message = c.String(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funds", t => t.Fundid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Userid)
                .Index(t => t.Userid)
                .Index(t => t.Fundid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FundTransactions", "Userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.FundTransactions", "Fundid", "dbo.Funds");
            DropIndex("dbo.FundTransactions", new[] { "Fundid" });
            DropIndex("dbo.FundTransactions", new[] { "Userid" });
            DropTable("dbo.FundTransactions");
        }
    }
}
