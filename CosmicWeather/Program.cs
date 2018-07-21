using System;
using CosmicWeather.Database;
using CosmicWeather.Model;

namespace CosmicWeather
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (CosmicWeatherDbContext db = new CosmicWeatherDbContext())
            {
                Planet planetTest = new Planet();
                planetTest.Name = "TESTING";
                db.Planets.Add(planetTest);
                db.SaveChanges();
            }
        }
    }
}
