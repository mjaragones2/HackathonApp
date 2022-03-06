namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFundComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Userid = c.String(maxLength: 128),
                        Comment = c.String(),
                        Fundid = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funds", t => t.Fundid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Userid)
                .Index(t => t.Userid)
                .Index(t => t.Fundid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FundComments", "Userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.FundComments", "Fundid", "dbo.Funds");
            DropIndex("dbo.FundComments", new[] { "Fundid" });
            DropIndex("dbo.FundComments", new[] { "Userid" });
            DropTable("dbo.FundComments");
        }
    }
}
