namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikeReacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Userid = c.String(maxLength: 128),
                        IsLiked = c.Boolean(nullable: false),
                        Fundid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funds", t => t.Fundid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Userid)
                .Index(t => t.Userid)
                .Index(t => t.Fundid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeReacts", "Userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeReacts", "Fundid", "dbo.Funds");
            DropIndex("dbo.LikeReacts", new[] { "Fundid" });
            DropIndex("dbo.LikeReacts", new[] { "Userid" });
            DropTable("dbo.LikeReacts");
        }
    }
}
