namespace Firewalls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        JoinDate = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieID = c.Int(nullable: false, identity: true),
                        MovieName = c.String(nullable: false, maxLength: 50),
                        Genre = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false),
                        ShowingDate = c.String(nullable: false),
                        Picture = c.Binary(),
                        Thea_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieID)
                .ForeignKey("dbo.Theatres", t => t.Thea_id, cascadeDelete: true)
                .Index(t => t.Thea_id);
            
            CreateTable(
                "dbo.Theatres",
                c => new
                    {
                        Theatre_id = c.Int(nullable: false, identity: true),
                        Theatre_name = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Theatre_id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        Seat_id = c.Int(nullable: false, identity: true),
                        Seat_state = c.Boolean(nullable: false),
                        Seat_cost = c.Int(nullable: false),
                        MovieID = c.Int(),
                        Thea_id = c.Int(),
                    })
                .PrimaryKey(t => t.Seat_id)
                .ForeignKey("dbo.Movies", t => t.MovieID)
                .ForeignKey("dbo.Theatres", t => t.Thea_id)
                .Index(t => t.MovieID)
                .Index(t => t.Thea_id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Ticket_id = c.Int(nullable: false, identity: true),
                        T_NO = c.String(),
                        Seat_id = c.Int(),
                        ID = c.Int(),
                    })
                .PrimaryKey(t => t.Ticket_id)
                .ForeignKey("dbo.Members", t => t.ID)
                .ForeignKey("dbo.Seats", t => t.Seat_id)
                .Index(t => t.Seat_id)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerName = c.String(),
                        LastName = c.String(),
                        Number = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.watchTrailers",
                c => new
                    {
                        Vid = c.Int(nullable: false, identity: true),
                        Vname = c.String(),
                        Vpath = c.String(),
                    })
                .PrimaryKey(t => t.Vid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "Seat_id", "dbo.Seats");
            DropForeignKey("dbo.Tickets", "ID", "dbo.Members");
            DropForeignKey("dbo.Seats", "Thea_id", "dbo.Theatres");
            DropForeignKey("dbo.Seats", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Movies", "Thea_id", "dbo.Theatres");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Tickets", new[] { "ID" });
            DropIndex("dbo.Tickets", new[] { "Seat_id" });
            DropIndex("dbo.Seats", new[] { "Thea_id" });
            DropIndex("dbo.Seats", new[] { "MovieID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Movies", new[] { "Thea_id" });
            DropTable("dbo.watchTrailers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tickets");
            DropTable("dbo.Seats");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Theatres");
            DropTable("dbo.Movies");
            DropTable("dbo.Members");
        }
    }
}
