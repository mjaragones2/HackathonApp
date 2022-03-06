namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingWithdrawReq : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WithdrawRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Userid = c.String(maxLength: 128),
                        Email = c.String(),
                        Contact = c.String(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Status = c.String(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Userid)
                .Index(t => t.Userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WithdrawRequests", "Userid", "dbo.AspNetUsers");
            DropIndex("dbo.WithdrawRequests", new[] { "Userid" });
            DropTable("dbo.WithdrawRequests");
        }
    }
}
