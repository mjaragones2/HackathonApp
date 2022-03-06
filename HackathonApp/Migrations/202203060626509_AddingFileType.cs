namespace HackathonApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFileType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundDocuments", "Filetype", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FundDocuments", "Filetype");
        }
    }
}
