namespace DodgingBranches.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommentRouteIdUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Route_RouteId", "dbo.Routes");
            DropIndex("dbo.Comments", new[] { "Route_RouteId" });
            RenameColumn(table: "dbo.Comments", name: "Route_RouteId", newName: "RouteId");
            AddColumn("dbo.Comments", "DateEntered", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "RouteId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "RouteId");
            AddForeignKey("dbo.Comments", "RouteId", "dbo.Routes", "RouteId", cascadeDelete: true);
            DropColumn("dbo.Comments", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "MyProperty", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "RouteId", "dbo.Routes");
            DropIndex("dbo.Comments", new[] { "RouteId" });
            AlterColumn("dbo.Comments", "RouteId", c => c.Int());
            DropColumn("dbo.Comments", "DateEntered");
            RenameColumn(table: "dbo.Comments", name: "RouteId", newName: "Route_RouteId");
            CreateIndex("dbo.Comments", "Route_RouteId");
            AddForeignKey("dbo.Comments", "Route_RouteId", "dbo.Routes", "RouteId");
        }
    }
}
