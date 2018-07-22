using System;
using System.Collections.Generic;
using System.Drawing;
using CosmicWeather.Database;
using CosmicWeather.Model;

namespace CosmicWeather
{
    public class Program
    {
        private static int _nDaysPerYear = 365;

        static void Main(string[] args)
        {
            //Data set creation
            //Assume each planet initial angle is 0 (planets are aligned)
            //Assume 1 year => 365 days
            Planet ferengi = new Planet { AngularSpeed = -1, Name = "Ferengi", StarDistance = 500 };
            Planet betasoide = new Planet { AngularSpeed = -3, Name = "Betasoide", StarDistance = 2000 };
            Planet vulcano = new Planet { AngularSpeed = 5, Name = "Vulcano", StarDistance = 1000 };

            StarSystem solarSystem = new StarSystem
            {
                Name = "Solar System",
                Planets = new List<Planet> { ferengi, betasoide, vulcano }
            };

            //TODO the number of days should come as an execution parameter
            List<Weather> testList = new List<Weather>(solarSystem.GetWeatherForXDays(10 * _nDaysPerYear, 10));

            //Persist results (USUALLY IN ANOTHER COMPONENT, BUT IS A TEST APP)
            //using (CosmicWeatherDbContext db = new CosmicWeatherDbContext())
            //{
            //    solarSystem.GetWeatherForXDays(10 * _nDaysPerYear, 4).ForEach(x=> db.Weathers.Add(x));
            //    db.SaveChanges();
            //}
        }
    }
}
