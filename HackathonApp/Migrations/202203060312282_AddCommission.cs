namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComisssionRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ComisssionRates");
        }
    }
}
