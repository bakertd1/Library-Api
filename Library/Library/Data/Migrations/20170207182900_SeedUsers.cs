using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Data.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                INSERT INTO [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'0845d1f5-251d-47fc-851b-ad99585bc70c', 0, N'61ef77f3-4295-4003-872c-0c44f87be84d', N'admin@library.com', 0, 1, NULL, N'ADMIN@LIBRARY.COM', N'ADMIN@LIBRARY.COM', N'AQAAAAEAACcQAAAAEOAOmVQutMaBDoECfMoAQjNC2lzxFKVZkYpDwc3OyHp52PZpBBD45C8EvbPzEEH+bw==', NULL, 0, N'e5e668a2-c6b2-4d24-83d1-97aafe25efab', 0, N'admin@library.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'fea795c6-6833-47e3-b9d8-cf1dd64102e7', 0, N'fc1ce914-a5d3-449c-8309-2029c2206213', N'user@library.com', 0, 1, NULL, N'USER@LIBRARY.COM', N'USER@LIBRARY.COM', N'AQAAAAEAACcQAAAAEP0qLYyLlkKKT8oWQNJacG4xmQ7BQMcXGgqmpFwDfsOjEymKOHEHsB6VFuy9u7D9Ag==', NULL, 0, N'e980c89a-5f2e-4b84-9e5c-ef073b21e44b', 0, N'user@library.com')

                INSERT INTO[dbo].[AspNetRoles]([Id], [Name]) VALUES(N'8b69fe95-aa8c-4e4e-8e30-dcf1fa06b61e', N'admin')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0845d1f5-251d-47fc-851b-ad99585bc70c', N'8b69fe95-aa8c-4e4e-8e30-dcf1fa06b61e')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
