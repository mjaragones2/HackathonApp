namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BdatUpdat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Bdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Bdate", c => c.String());
        }
    }
}
