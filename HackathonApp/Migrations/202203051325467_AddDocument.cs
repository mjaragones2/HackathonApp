namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocument : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Fundid = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funds", t => t.Fundid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.Fundid)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FundDocuments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FundDocuments", "Fundid", "dbo.Funds");
            DropIndex("dbo.FundDocuments", new[] { "UserId" });
            DropIndex("dbo.FundDocuments", new[] { "Fundid" });
            DropTable("dbo.FundDocuments");
        }
    }
}
