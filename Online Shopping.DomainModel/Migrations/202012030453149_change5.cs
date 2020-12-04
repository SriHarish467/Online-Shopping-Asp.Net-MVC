namespace Online_Shopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Carts", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
