namespace Mar9_一對多.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpermissioncolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Authority", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Authority");
        }
    }
}
