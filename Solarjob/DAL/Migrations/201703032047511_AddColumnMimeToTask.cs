namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnMimeToTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "MimeType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "MimeType");
        }
    }
}
