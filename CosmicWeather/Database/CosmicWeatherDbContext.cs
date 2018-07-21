using System.Data.Entity;
using CosmicWeather.Model;

namespace CosmicWeather.Database
{
    public class CosmicWeatherDbContext : DbContext
    {
        public CosmicWeatherDbContext() : base("name=CosmicWeatherDbContext")
        {
        }

        public virtual DbSet<Planet> Planets { get; set; }
    }
}
