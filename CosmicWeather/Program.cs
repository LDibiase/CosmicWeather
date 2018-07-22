using System;
using System.Collections.Generic;
using CosmicWeather.Database;
using CosmicWeather.Model;

namespace CosmicWeather
{
    public class Program
    {
        private static readonly int _nDaysPerYear = 365;
        private static int _amountYears;
        private static int _precision = -1;

        static void Main(string[] args)
        {
            if (args != null)
            {
                _amountYears = Convert.ToInt32(args[0]);

                if (args.Length > 1)
                    _precision = Convert.ToInt32(args[1]);
            }

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

            //Persist results (USUALLY IN ANOTHER COMPONENT, BUT IS A TEST APP)
            using (CosmicWeatherDbContext db = new CosmicWeatherDbContext())
            {
                if(_precision >= 0)
                    solarSystem.GetWeatherForXDays(_amountYears * _nDaysPerYear, _precision).ForEach(x => db.Weathers.Add(x));
                else
                    solarSystem.GetWeatherForXDays(_amountYears * _nDaysPerYear).ForEach(x => db.Weathers.Add(x));

                db.SaveChanges();
            }
        }
    }
}
