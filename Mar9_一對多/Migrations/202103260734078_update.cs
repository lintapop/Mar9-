namespace Mar9_一對多.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "passwordSalt", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "passwordSalt", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
