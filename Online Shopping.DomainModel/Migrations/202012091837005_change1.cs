namespace Online_Shopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderDetails");
            DropColumn("dbo.OrderDetails", "OrderDetailsId");
            AddColumn("dbo.OrderDetails", "OrderDetailId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrderDetails", "OrderDetailId");
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "OrderDetailsId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropPrimaryKey("dbo.OrderDetails");
            DropColumn("dbo.OrderDetails", "OrderId");
            DropColumn("dbo.OrderDetails", "OrderDetailId");
            AddPrimaryKey("dbo.OrderDetails", "OrderDetailsId");
        }
    }
}
