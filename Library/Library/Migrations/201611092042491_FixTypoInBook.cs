namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTypoInBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "PublicationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "PublicatonDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "PublicatonDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "PublicationDate");
        }
    }
}
