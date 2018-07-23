using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using CosmicWeather.Helpers;

namespace CosmicWeather.Model
{
    public class StarSystem
    {
        private static readonly CoordinatesHelper Star = new CoordinatesHelper { PositionX = 0, PositionY = 0 }; //Sun
        private static readonly MathHelper Math = new MathHelper(); //Math calculations helper

        #region Properties

        [StringLength(50)] public string Name { get; set; }

        public List<Planet> Planets { get; set; }

        #endregion

        #region Methods

        private bool IsDrought(List<CoordinatesHelper> coordinates)
        {
            List<CoordinatesHelper> coordinatesAux = new List<CoordinatesHelper>(coordinates)
            {
                //Planets and sun must be aligned
                Star
            };

            return Math.AreCoordinatesAligned(coordinatesAux);
        }

        private bool IsDrought(List<CoordinatesHelper> coordinates, int nFractionalDigits)
        {
            List<CoordinatesHelper> coordinatesAux = new List<CoordinatesHelper>(coordinates)
            {
                //Planets and sun must be aligned
                Star
            };

            return Math.AreCoordinatesAligned(coordinatesAux, nFractionalDigits);
        }

        private bool IsOptimum(List<CoordinatesHelper> coordinates)
        {
            //Planets must be aligned
            return Math.AreCoordinatesAligned(coordinates);
        }

        private bool IsOptimum(List<CoordinatesHelper> coordinates, int nFractionalDigits)
        {
            //Planets must be aligned
            return Math.AreCoordinatesAligned(coordinates, nFractionalDigits);
        }

        private bool IsRain(List<CoordinatesHelper> coordinates)
        {
            if (coordinates.Count > 3) //Method should throw an exception as the analyzed figure wouldn't be a triangle
            {
                return false;
            }

            //The orientation of all the triangles drawn by each different combination between point1, point2, point3 and the star (P1P2P3, P1P2S, P1P3S and P3P1S) must match
            return Math.PositiveOrientation(coordinates[0], coordinates[1], coordinates[2]) ==
                   Math.PositiveOrientation(coordinates[0], coordinates[1], Star) ==
                   Math.PositiveOrientation(coordinates[0], coordinates[2], Star) ==
                   Math.PositiveOrientation(coordinates[2], coordinates[0], Star);
        }

        private Weather GetWeather(int day, ref double maxPerimeter, ref int maxRainDay)
        {
            List<CoordinatesHelper> coordinates = new List<CoordinatesHelper>();

            //Get coordinates list
            Planets.ForEach(planet => coordinates.Add(planet.CurrentCoordinates(day)));

            if (IsDrought(coordinates))
                return new Weather { DayNumber = day, WeatherType = WeatherEnum.sequia };
            if (IsOptimum(coordinates))
                return new Weather { DayNumber = day, WeatherType = WeatherEnum.optimo };
            if (IsRain(coordinates))
            {
                double perimeter = Math.Perimeter(coordinates[0], coordinates[1], coordinates[2]);

                if (perimeter > maxPerimeter)
                {
                    maxPerimeter = perimeter;
                    maxRainDay = day;
                }

                return new Weather {DayNumber = day, WeatherType = WeatherEnum.lluvia};
            }

            return new Weather { DayNumber = day, WeatherType = WeatherEnum.normal };
        }

        private Weather GetWeather(int day, ref double maxPerimeter, ref int maxRainDay, int nFractionalDigits)
        {
            List<CoordinatesHelper> coordinates = new List<CoordinatesHelper>();

            //Get coordinates list
            Planets.ForEach(planet => coordinates.Add(planet.CurrentCoordinates(day, nFractionalDigits)));

            if (IsDrought(coordinates, nFractionalDigits))
                return new Weather { DayNumber = day, WeatherType = WeatherEnum.sequia };
            if (IsOptimum(coordinates, nFractionalDigits))
                return new Weather { DayNumber = day, WeatherType = WeatherEnum.optimo };
            if (IsRain(coordinates))
            {
                double perimeter = Math.Perimeter(coordinates[0], coordinates[1], coordinates[2]);

                if (perimeter > maxPerimeter)
                {
                    maxPerimeter = perimeter;
                    maxRainDay = day;
                }

                return new Weather { DayNumber = day, WeatherType = WeatherEnum.lluvia };
            }

            return new Weather { DayNumber = day, WeatherType = WeatherEnum.normal };
        }

        private void SetMaxRain(List<Weather> weatherList, int maxRainDay)
        {
            //Get maximum rain day
            Weather maxRain = weatherList.Find(weather => weather.DayNumber == maxRainDay);

            //Update weather type
            maxRain.WeatherType = WeatherEnum.lluviaMaxima;
        }

        public List<Weather> GetWeatherForXDays(int days)
        {
            double maxPerimeter = 0.0; //Maximum perimeter
            int maxRainDay = -1; //Day of maximum rain

            List<Weather> weatherList = new List<Weather>();

            //Sort planets by distance to star in descending order
            Planets.Sort((planet1,planet2) => planet2.StarDistance.CompareTo(planet1.StarDistance));

            for (int day = 1; day <= days; day++)
            {
                weatherList.Add(GetWeather(day, ref maxPerimeter, ref maxRainDay));
            }

            SetMaxRain(weatherList, maxRainDay);

            return weatherList;
        }

        public List<Weather> GetWeatherForXDays(int days, int nFractionalDigits)
        {
            double maxPerimeter = 0.0; //Maximum perimeter
            int maxRainDay = -1; //Day of maximum rain

            List<Weather> weatherList = new List<Weather>();

            //Sort planets by distance to star in descending order
            Planets.Sort((planet1, planet2) => planet2.StarDistance.CompareTo(planet1.StarDistance));

            for (int day = 1; day <= days; day++)
            {
                weatherList.Add(GetWeather(day, ref maxPerimeter, ref maxRainDay, nFractionalDigits));
            }

            SetMaxRain(weatherList, maxRainDay);

            return weatherList;
        }

        #endregion
    }
}
