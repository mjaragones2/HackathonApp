namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFund : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Funds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Story = c.String(nullable: false),
                        AmountNeeded = c.Decimal(precision: 18, scale: 2),
                        AmountAcquired = c.Decimal(precision: 18, scale: 2),
                        DateEnd = c.DateTime(),
                        DateCreated = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Funds");
        }
    }
}
