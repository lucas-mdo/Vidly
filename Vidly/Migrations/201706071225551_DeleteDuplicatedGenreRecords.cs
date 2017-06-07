namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDuplicatedGenreRecords : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Genres SET Name = 'Horror' WHERE Id = 8");
            Sql("UPDATE Genres SET Name = 'Mystery' WHERE Id = 9");
            Sql("UPDATE Genres SET Name = 'Romance' WHERE Id = 10");
            Sql("UPDATE Genres SET Name = 'Science Fiction' WHERE Id = 11");
            Sql("UPDATE Genres SET Name = 'Thriller' WHERE Id = 12");

            Sql("DELETE FROM Genres WHERE Id > 12");
        }
        
        public override void Down()
        {
        }
    }
}
