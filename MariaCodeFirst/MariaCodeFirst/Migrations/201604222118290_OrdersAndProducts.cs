namespace MariaCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersAndProducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "TestDB.OrderProducts",
                c => new
                    {
                        OrderProductId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.OrderProductId)
                .ForeignKey("TestDB.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("TestDB.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "TestDB.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("TestDB.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "TestDB.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                        Description = c.String(nullable: false, maxLength: 150, storeType: "nvarchar"),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("TestDB.OrderProducts", "ProductId", "TestDB.Products");
            DropForeignKey("TestDB.OrderProducts", "OrderId", "TestDB.Orders");
            DropForeignKey("TestDB.Orders", "CustomerId", "TestDB.Customers");
            DropIndex("TestDB.Orders", new[] { "CustomerId" });
            DropIndex("TestDB.OrderProducts", new[] { "ProductId" });
            DropIndex("TestDB.OrderProducts", new[] { "OrderId" });
            DropTable("TestDB.Products");
            DropTable("TestDB.Orders");
            DropTable("TestDB.OrderProducts");
        }
    }
}
