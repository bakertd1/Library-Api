namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"

                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'89aee311-f8df-4ad1-b9ba-7c7dd7a52650', N'user@test.com', 1, N'ANARuvWxBNOOssFA8obSBGbzbcMuhsreu6sC3UgQ/1g2UeUIhY0q5bekH2pZ/41Q2g==', N'b9c4fd76-9f00-4c29-acb5-4ba5af3f2b5d', N'1111111111', 0, 0, NULL, 0, 0, N'user@test.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8c49ae8d-314e-4e1a-84d7-2d6c4a61d544', N'admin@test.com', 1, N'AE2ZLnT7KdcmIFWovRHvaJaOAVtyiralVLMzWm8m+I+lixXBVbwmOuWAxzjkPQSrXg==', N'727295b9-b1f1-4265-b363-2c8a5d2ad8cf', N'1111111111', 0, 0, NULL, 0, 0, N'admin@test.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8b69fe95-aa8c-4e4e-8e30-dcf1fa06b61e', N'admin')
                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8c49ae8d-314e-4e1a-84d7-2d6c4a61d544', N'8b69fe95-aa8c-4e4e-8e30-dcf1fa06b61e')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
