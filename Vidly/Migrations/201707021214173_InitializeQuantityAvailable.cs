namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeQuantityAvailable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Movies SET QuantityAvailable = QuantityInStock");
        }
        
        public override void Down()
        {
        }
    }
}
