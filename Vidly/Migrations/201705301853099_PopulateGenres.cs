namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Adventure')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Comedy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Crime')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Drama')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6, 'Fantasy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (7, 'Historical')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (8, 'Historical')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (9, 'Historical')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (10, 'Horror')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (11, 'Mystery')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (12, 'Romance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (13, 'Science Fiction')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (14, 'Thriller')");

        }
        
        public override void Down()
        {
        }
    }
}
