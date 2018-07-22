using System.Data.Entity;
using CosmicWeather.Model;

namespace CosmicWeather.Database
{
    public class CosmicWeatherDbContext : DbContext
    {
        public CosmicWeatherDbContext() : base("name=CosmicWeatherDbContext")
        {
        }
        public virtual DbSet<Weather> Weathers { get; set; }
        public virtual DbSet<WeatherPeriod> WeatherPeriods { get; set; }
    }
}
