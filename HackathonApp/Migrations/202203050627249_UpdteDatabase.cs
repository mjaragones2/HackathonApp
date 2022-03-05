namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdteDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Bdate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Bdate", c => c.DateTime());
        }
    }
}
