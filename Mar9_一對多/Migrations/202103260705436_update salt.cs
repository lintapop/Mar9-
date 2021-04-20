namespace Mar9_一對多.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "passwordSalt", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "passwordSalt");
        }
    }
}
