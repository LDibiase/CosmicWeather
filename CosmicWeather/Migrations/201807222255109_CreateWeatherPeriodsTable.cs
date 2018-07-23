namespace CosmicWeather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateWeatherPeriodsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeatherPeriods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WeatherType = c.Int(nullable: false),
                        AmountPeriods = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WeatherPeriods");
        }
    }
}
