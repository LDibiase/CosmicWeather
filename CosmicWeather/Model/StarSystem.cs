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
        private int _nFractionalDigits; //Number of fractional digits to be used when performing calculations
        private static readonly CoordinatesHelper Star = new CoordinatesHelper { PositionX = 0, PositionY = 0 }; //Sun
        private double _maxPerimeter;
        private int _maxRainDay;

        #region Properties

        [StringLength(50)] public string Name { get; set; }

        public List<Planet> Planets { get; set; }

        #endregion

        #region Methods

        private double StraightLineSlope(CoordinatesHelper point1, CoordinatesHelper point2, int nFractionalDigits)
        {
            //Straight line slope
            return Math.Round((point2.PositionY - point1.PositionY) / (point2.PositionX - point1.PositionX), nFractionalDigits);
        }

        private bool AreCoordinatesAligned(List<CoordinatesHelper> coordinates)
        {
            //if (coordinates.Count <= 1) //True if the list is empty
            //    return true;

            //double slope = StraightLineSlope(coordinates[0], coordinates[1], _nFractionalDigits);

            //for (int i = 2; i < coordinates.Count; i++)
            //{
            //    if (slope != StraightLineSlope(coordinates[i - 1], coordinates[i], _nFractionalDigits))
            //        return false;
            //}

            //return true;

            //The slope of the straight line drawn by Poin1 and Point2 must match with the slope of the straight line drawn by Point2 and Point3
            return StraightLineSlope(coordinates[0], coordinates[1], _nFractionalDigits) == StraightLineSlope(coordinates[1], coordinates[2], _nFractionalDigits);
        }

        private bool PositiveOrientation(CoordinatesHelper point1, CoordinatesHelper point2, CoordinatesHelper point3)
        {
            //Triangle Point1Point2Point3 orientation
            return ((point1.PositionX - point3.PositionX) * (point2.PositionY - point3.PositionY)) -
                   ((point1.PositionY - point3.PositionY) * (point2.PositionX - point3.PositionX)) >= 0;
        }

        private double Distance(CoordinatesHelper point1, CoordinatesHelper point2)
        {
            //Distance between two points
            return Math.Sqrt(Math.Pow(point1.PositionX - point2.PositionX, 2) +
                             Math.Pow(point1.PositionY - point2.PositionY, 2));
        }

        private double Perimeter(CoordinatesHelper point1, CoordinatesHelper point2, CoordinatesHelper point3)
        {
            //Triangle Point1Point2Point3 perimeter
            return Distance(point1, point2) + Distance(point2, point3) + Distance(point3, point1);
        }

        private bool IsDrought(List<CoordinatesHelper> coordinates)
        {
            List<CoordinatesHelper> coordinatesAux = new List<CoordinatesHelper>(coordinates)
            {
                //Planets and sun must be aligned
                Star
            };

            return AreCoordinatesAligned(coordinatesAux);
        }

        private bool IsOptimum(List<CoordinatesHelper> coordinates)
        {
            //Planets must be aligned
            return AreCoordinatesAligned(coordinates);
        }

        private bool IsRain(List<CoordinatesHelper> coordinates)
        {
            CoordinatesHelper point1 = coordinates[0];
            CoordinatesHelper point2 = coordinates[1];
            CoordinatesHelper point3 = coordinates[2];

            //All triangles (P1P2P3, P1P2S, P1P3S and P3P1S) orientations must match
            return PositiveOrientation(point1, point2, point3) == PositiveOrientation(point1, point2, Star) ==
                   PositiveOrientation(point1, point3, Star) == PositiveOrientation(point3, point1, Star);
        }

        private Weather GetWeather(int day)
        {
            List<CoordinatesHelper> coordinates = new List<CoordinatesHelper>();

            //Get coordinates list
            //Planets.ForEach(planet => coordinates.Add(planet.CurrentCoordinates(day, _nFractionalDigits)));
            Planets.ForEach(planet => coordinates.Add(planet.CurrentCoordinates(day)));

            if (IsDrought(coordinates))
                return new Weather { DayNumber = day, WeatherType = WeatherEnum.Drought };
            if (IsOptimum(coordinates))
                return new Weather { DayNumber = day, WeatherType = WeatherEnum.Optimum };
            if (IsRain(coordinates))
            {
                double perimeter = Perimeter(coordinates[0], coordinates[1], coordinates[2]);

                if (perimeter > _maxPerimeter)
                {
                    _maxPerimeter = perimeter;
                    _maxRainDay = day;
                }

                return new Weather {DayNumber = day, WeatherType = WeatherEnum.Rain};
            }

            return new Weather { DayNumber = day, WeatherType = WeatherEnum.Normal };
        }

        private void SetMaxRain(List<Weather> weatherList)
        {
            //Get maximum rain day
            Weather maxRain = weatherList.Find(weather => weather.DayNumber.Equals(_maxRainDay));

            //Update weather type
            maxRain.WeatherType = WeatherEnum.MaxRain;
        }

        public List<Weather> GetWeatherForXDays(int days, int nFractionalDigits)
        {
            _nFractionalDigits = nFractionalDigits; //Number of fractional digits to be used when performing calculations
            _maxPerimeter = 0.0;
            _maxRainDay = -1;

            List<Weather> weatherList = new List<Weather>();

            //Sort planets by distance to star in descending order
            Planets.Sort((planet1,planet2) => planet2.StarDistance.CompareTo(planet1.StarDistance));

            for (int day = 1; day <= days; day++)
            {
                weatherList.Add(GetWeather(day));
            }

            SetMaxRain(weatherList);

            return weatherList;
        }

        #endregion
    }
}
