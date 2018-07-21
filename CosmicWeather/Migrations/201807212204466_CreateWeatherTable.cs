namespace CosmicWeather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateWeatherTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WeatherType = c.Int(nullable: false),
                        DayNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Weathers");
        }
    }
}
