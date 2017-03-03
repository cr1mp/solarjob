namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnsToTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Type", c => c.String());
            AddColumn("dbo.Tasks", "Executor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Executor");
            DropColumn("dbo.Tasks", "Type");
        }
    }
}
