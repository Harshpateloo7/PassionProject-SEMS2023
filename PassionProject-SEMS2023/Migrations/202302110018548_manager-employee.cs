namespace PassionProject_SEMS2023.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manageremployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerID = c.Int(nullable: false, identity: true),
                        ManagerName = c.String(),
                        ManagerBranch = c.String(),
                        ManagerPosition = c.String(),
                    })
                .PrimaryKey(t => t.ManagerID);
            
            CreateTable(
                "dbo.ManagerEmployees",
                c => new
                    {
                        Manager_ManagerID = c.Int(nullable: false),
                        Employee_EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Manager_ManagerID, t.Employee_EmployeeID })
                .ForeignKey("dbo.Managers", t => t.Manager_ManagerID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeID, cascadeDelete: true)
                .Index(t => t.Manager_ManagerID)
                .Index(t => t.Employee_EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManagerEmployees", "Employee_EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.ManagerEmployees", "Manager_ManagerID", "dbo.Managers");
            DropIndex("dbo.ManagerEmployees", new[] { "Employee_EmployeeID" });
            DropIndex("dbo.ManagerEmployees", new[] { "Manager_ManagerID" });
            DropTable("dbo.ManagerEmployees");
            DropTable("dbo.Managers");
        }
    }
}
