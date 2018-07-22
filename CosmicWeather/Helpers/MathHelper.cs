using System;
using System.Collections.Generic;

namespace CosmicWeather.Helpers
{
    public class MathHelper
    {
        public double StraightLineSlope(CoordinatesHelper point1, CoordinatesHelper point2)
        {
            //Returns the slope of the straight line drawn by point1 and point2
            return (point2.PositionY - point1.PositionY) / (point2.PositionX - point1.PositionX);
        }

        public double StraightLineSlope(CoordinatesHelper point1, CoordinatesHelper point2, int nFractionalDigits)
        {
            //Returns the slope of the straight line drawn by point1 and point2 rounded to n fractional digits
            return Math.Round(StraightLineSlope(point1, point2), nFractionalDigits);
        }

        public bool AreCoordinatesAligned(List<CoordinatesHelper> coordinates, int _nFractionalDigits)
        {
            if (coordinates.Count <= 1) //True if the list cointains only one point or if it's empty
                return true;

            //The slope of the straight line drawn by each pair of points rounded to n fractional digits must match
            double slope = StraightLineSlope(coordinates[0], coordinates[1], _nFractionalDigits);

            for (int i = 2; i < coordinates.Count; i++)
            {
                if (slope != StraightLineSlope(coordinates[i - 1], coordinates[i], _nFractionalDigits))
                    return false;
            }

            return true;
        }

        public bool AreCoordinatesAligned(List<CoordinatesHelper> coordinates)
        {
            if (coordinates.Count <= 1) //True if the list cointains only one point or if it's empty
                return true;

            //The slope of the straight line drawn by each pair of points must match
            double slope = StraightLineSlope(coordinates[0], coordinates[1]);

            for (int i = 2; i < coordinates.Count; i++)
            {
                if (slope != StraightLineSlope(coordinates[i - 1], coordinates[i]))
                    return false;
            }

            return true;
        }

        public bool PositiveOrientation(CoordinatesHelper point1, CoordinatesHelper point2, CoordinatesHelper point3)
        {
            //Returns the orientation of the triangle drawn by point1, point2 and point3
            return ((point1.PositionX - point3.PositionX) * (point2.PositionY - point3.PositionY)) -
                   ((point1.PositionY - point3.PositionY) * (point2.PositionX - point3.PositionX)) >= 0;
        }

        public double Distance(CoordinatesHelper point1, CoordinatesHelper point2)
        {
            //Returns the distance between point1 and point2
            return Math.Sqrt(Math.Pow(point1.PositionX - point2.PositionX, 2) +
                             Math.Pow(point1.PositionY - point2.PositionY, 2));
        }

        public double Perimeter(CoordinatesHelper point1, CoordinatesHelper point2, CoordinatesHelper point3)
        {
            //Returns the perimeter of the triangle drawn by point1, point2 and point3
            return Distance(point1, point2) + Distance(point2, point3) + Distance(point3, point1);
        }
    }
}
