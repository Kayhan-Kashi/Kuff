namespace Kuff.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                        FirstName = c.String(),
                        LastName = c.String(),
                        TelephoneNumber = c.String(),
                        CityState = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        PostalCode = c.String(),
                        Company = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
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
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        Code = c.String(),
                        Title = c.String(nullable: false),
                        DateOfCreation = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductTypeProperties",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductTypeId = c.Guid(nullable: false),
                        DataTypeId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        IsUserDecision = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataTypes", t => t.DataTypeId)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId)
                .Index(t => t.ProductTypeId)
                .Index(t => t.DataTypeId);
            
            CreateTable(
                "dbo.DataTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ControlToRender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductPriceId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        OrderItemPictureId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PriceWithoutDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceWithDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderItemDescription = c.String(),
                        ProductSummary = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.ProductPrices", t => t.ProductPriceId)
                .Index(t => t.ProductPriceId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PaymentId = c.Guid(nullable: false),
                        ShipmentCostId = c.Guid(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Date = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserCommentsAboutOrder = c.String(),
                        FinalPriceWithShipmentCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShipmentCosts", t => t.ShipmentCostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ShipmentCostId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PaymentMethodId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        IsPayingDone = c.Boolean(nullable: false),
                        DateOfPayment = c.String(),
                        PayingAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodId)
                .ForeignKey("dbo.Orders", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.PaymentMethodId);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShipmentCosts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ShipmentMethodId = c.Guid(nullable: false),
                        DepartureCity = c.String(),
                        DestinationCity = c.String(),
                        DateOfAddedShipmentCost = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShipmentMethods", t => t.ShipmentMethodId)
                .Index(t => t.ShipmentMethodId);
            
            CreateTable(
                "dbo.ShipmentMethods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItemSpecifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderItemId = c.Guid(nullable: false),
                        Title = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderItems", t => t.OrderItemId, cascadeDelete: true)
                .Index(t => t.OrderItemId);
            
            CreateTable(
                "dbo.ProductPrices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Date = c.String(),
                        PriceWithoutDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceWithDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductTypeId = c.Guid(nullable: false),
                        DateOfAdding = c.String(),
                        Summary = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.ProductAvailabilities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Date = c.String(),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductPictures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        FileName = c.String(),
                        FilePath = c.String(),
                        ContentType = c.String(),
                        FileExtension = c.String(),
                        PictureOrder = c.Int(nullable: false),
                        IsForSummaryPreview = c.Boolean(nullable: false),
                        IsMainPicture = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductPropertyValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        ProductTypePropertyId = c.Guid(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.ProductTypeProperties", t => t.ProductTypePropertyId)
                .Index(t => t.ProductId)
                .Index(t => t.ProductTypePropertyId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderItems", "ProductPriceId", "dbo.ProductPrices");
            DropForeignKey("dbo.ProductPrices", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductPropertyValues", "ProductTypePropertyId", "dbo.ProductTypeProperties");
            DropForeignKey("dbo.ProductPropertyValues", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductPictures", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductAvailabilities", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItemSpecifications", "OrderItemId", "dbo.OrderItems");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ShipmentCostId", "dbo.ShipmentCosts");
            DropForeignKey("dbo.ShipmentCosts", "ShipmentMethodId", "dbo.ShipmentMethods");
            DropForeignKey("dbo.Payments", "Id", "dbo.Orders");
            DropForeignKey("dbo.Payments", "PaymentMethodId", "dbo.PaymentMethods");
            DropForeignKey("dbo.ProductTypeProperties", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductTypeProperties", "DataTypeId", "dbo.DataTypes");
            DropForeignKey("dbo.ProductTypes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProductPropertyValues", new[] { "ProductTypePropertyId" });
            DropIndex("dbo.ProductPropertyValues", new[] { "ProductId" });
            DropIndex("dbo.ProductPictures", new[] { "ProductId" });
            DropIndex("dbo.ProductAvailabilities", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.ProductPrices", new[] { "ProductId" });
            DropIndex("dbo.OrderItemSpecifications", new[] { "OrderItemId" });
            DropIndex("dbo.ShipmentCosts", new[] { "ShipmentMethodId" });
            DropIndex("dbo.Payments", new[] { "PaymentMethodId" });
            DropIndex("dbo.Payments", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "ShipmentCostId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "ProductPriceId" });
            DropIndex("dbo.ProductTypeProperties", new[] { "DataTypeId" });
            DropIndex("dbo.ProductTypeProperties", new[] { "ProductTypeId" });
            DropIndex("dbo.ProductTypes", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductPropertyValues");
            DropTable("dbo.ProductPictures");
            DropTable("dbo.ProductAvailabilities");
            DropTable("dbo.Products");
            DropTable("dbo.ProductPrices");
            DropTable("dbo.OrderItemSpecifications");
            DropTable("dbo.ShipmentMethods");
            DropTable("dbo.ShipmentCosts");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.Payments");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.DataTypes");
            DropTable("dbo.ProductTypeProperties");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Categories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
        }
    }
}
