namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddinEWallet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EWallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Userid = c.String(maxLength: 128),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Userid)
                .Index(t => t.Userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EWallets", "Userid", "dbo.AspNetUsers");
            DropIndex("dbo.EWallets", new[] { "Userid" });
            DropTable("dbo.EWallets");
        }
    }
}
