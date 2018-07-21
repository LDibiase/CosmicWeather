namespace CosmicWeather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePlanetsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Planets");
        }
    }
}
