namespace Online_Shopping.DomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ResetCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ResetCode");
        }
    }
}
