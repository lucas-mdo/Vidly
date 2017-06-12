namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2766f125-3aa8-44bb-a072-6ae85018a990', N'guest@vidly.com', 0, N'AKGu3iE94KW1s6OG6nWZZsS5d7FaeC2aRLIcB9uZF8ib4xLoeekY2M7tqex/IsCI6w==', N'f04859fb-033c-4bfc-bdba-6e4f409811eb', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fdddadc8-d1d5-407a-bc60-531eec73daa8', N'admin@vidly.com', 0, N'ALfmwd+tGiYaX1r1x7oNgJ0K63d5i0FTBX3cQ/WmQM0J1bsgjSyMDnaGHUVqL4rjmg==', N'bd1f3dda-5473-4a12-9161-15a8681de8f0', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'226e88e3-5460-4206-b557-33d36e50e733', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fdddadc8-d1d5-407a-bc60-531eec73daa8', N'226e88e3-5460-4206-b557-33d36e50e733')
");
        }
        
        public override void Down()
        {
        }
    }
}
