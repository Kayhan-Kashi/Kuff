namespace Kuff.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderItemDecription : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderItems", "OrderItemDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "OrderItemDescription", c => c.String());
        }
    }
}
