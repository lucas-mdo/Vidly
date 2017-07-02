namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuantityAvailableToMovies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "QuantityAvailable", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "QuantityAvailable");
        }
    }
}
