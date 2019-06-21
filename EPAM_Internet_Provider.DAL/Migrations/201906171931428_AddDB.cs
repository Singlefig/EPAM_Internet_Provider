namespace EPAM_Internet_Provider.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        RateId = c.Int(nullable: false, identity: true),
                        RateName = c.String(nullable: false, maxLength: 20),
                        RateCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Service_ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.RateId)
                .ForeignKey("dbo.Services", t => t.Service_ServiceId)
                .Index(t => t.Service_ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        ServiceType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 20),
                        ConfirmPassword = c.String(),
                        Name = c.String(),
                        RememberMe = c.Boolean(nullable: false),
                        Account = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Subscription",
                c => new
                    {
                        SubsId = c.Int(nullable: false, identity: true),
                        ServiceBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsBlocked = c.Boolean(nullable: false),
                        Service_ServiceId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.SubsId)
                .ForeignKey("dbo.Services", t => t.Service_ServiceId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Service_ServiceId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscription", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Subscription", "Service_ServiceId", "dbo.Services");
            DropForeignKey("dbo.Rates", "Service_ServiceId", "dbo.Services");
            DropIndex("dbo.Subscription", new[] { "User_UserId" });
            DropIndex("dbo.Subscription", new[] { "Service_ServiceId" });
            DropIndex("dbo.Rates", new[] { "Service_ServiceId" });
            DropTable("dbo.Subscription");
            DropTable("dbo.Users");
            DropTable("dbo.Services");
            DropTable("dbo.Rates");
        }
    }
}
