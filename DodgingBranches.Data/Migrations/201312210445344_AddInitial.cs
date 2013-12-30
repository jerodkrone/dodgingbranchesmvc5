namespace DodgingBranches.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Address1 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(),
                        MyProperty = c.Int(nullable: false),
                        UserId = c.String(),
                        ParentCommentId = c.Int(),
                        Route_RouteId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Routes", t => t.Route_RouteId)
                .Index(t => t.Route_RouteId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartPoint = c.Geography(),
                        EndPoint = c.Geography(),
                        DateEntered = c.DateTime(nullable: false),
                        UserId = c.String(),
                        EndLocation_AddressId = c.Int(),
                        StartLocation_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.RouteId)
                .ForeignKey("dbo.Addresses", t => t.EndLocation_AddressId)
                .ForeignKey("dbo.Addresses", t => t.StartLocation_AddressId)
                .Index(t => t.EndLocation_AddressId)
                .Index(t => t.StartLocation_AddressId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagText = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TagRoutes",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Route_RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Route_RouteId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.Route_RouteId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Route_RouteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagRoutes", "Route_RouteId", "dbo.Routes");
            DropForeignKey("dbo.TagRoutes", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.Routes", "StartLocation_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Routes", "EndLocation_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Comments", "Route_RouteId", "dbo.Routes");
            DropIndex("dbo.TagRoutes", new[] { "Route_RouteId" });
            DropIndex("dbo.TagRoutes", new[] { "Tag_TagId" });
            DropIndex("dbo.Routes", new[] { "StartLocation_AddressId" });
            DropIndex("dbo.Routes", new[] { "EndLocation_AddressId" });
            DropIndex("dbo.Comments", new[] { "Route_RouteId" });
            DropTable("dbo.TagRoutes");
            DropTable("dbo.Tags");
            DropTable("dbo.Routes");
            DropTable("dbo.Comments");
            DropTable("dbo.Addresses");
        }
    }
}
