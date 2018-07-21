using System;
using System.Collections.Generic;
using CosmicWeather.Database;
using CosmicWeather.Model;

namespace CosmicWeather
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Data set creation
            //Assume initial angle as 0 (every planet is aligned)
            //Assume 1 year => 365 days
            Planet vulcano = new Planet { AngularSpeed = 5, InitialAngle = 0, Name = "Vulcano", StarDistance = 1000 };
            Planet ferengi = new Planet { AngularSpeed = -1, InitialAngle = 0, Name = "Ferengi", StarDistance = 500 };
            Planet betasoide = new Planet { AngularSpeed = -3, InitialAngle = 0, Name = "Betasoide", StarDistance = 2000 };

            StarSystem solarSystem = new StarSystem
            {
                Name = "Solar System",
                Planets = new List<Planet> {vulcano, ferengi, betasoide},
                Weathers = new List<Weather>()
            };

            //TODO the number of days should come as execution parameter
            List<Weather> weathers = new List<Weather>(solarSystem.GetWeatherForDays(3650));

            //Persist results (USUALLY IN ANOTHER COMPONENT, BUT IS A TEST APP)
            using (CosmicWeatherDbContext db = new CosmicWeatherDbContext())
            {
                weathers.ForEach(x=> db.Weathers.Add(x));
                db.SaveChanges();
            }
        }
    }
}
