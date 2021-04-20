namespace Mar9_一對多.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepID = c.Int(nullable: false),
                        customerName = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        tel = c.String(nullable: false, maxLength: 50),
                        email = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepID, cascadeDelete: true)
                .Index(t => t.DepID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "DepID", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "DepID" });
            DropTable("dbo.Users");
            DropTable("dbo.Departments");
        }
    }
}
