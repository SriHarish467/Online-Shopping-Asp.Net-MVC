namespace Online_Shopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PhoneNumber", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "PhoneNumber");
        }
    }
}
