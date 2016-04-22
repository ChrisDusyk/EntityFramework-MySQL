namespace MariaCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersAndProducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "OrderProducts",
                c => new
                    {
                        OrderProductId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.OrderProductId)
                .ForeignKey("Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "Products",
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
            DropForeignKey("OrderProducts", "ProductId", "Products");
            DropForeignKey("OrderProducts", "OrderId", "Orders");
            DropForeignKey("Orders", "CustomerId", "Customers");
            DropIndex("Orders", new[] { "CustomerId" });
            DropIndex("OrderProducts", new[] { "ProductId" });
            DropIndex("OrderProducts", new[] { "OrderId" });
            DropTable("Products");
            DropTable("Orders");
            DropTable("OrderProducts");
            MoveTable(name: "Customers", newSchema: "TestDB");
        }
    }
}
