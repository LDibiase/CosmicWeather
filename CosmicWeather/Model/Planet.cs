using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using CosmicWeather.Helpers;

namespace CosmicWeather.Model
{
    public class Planet
    {
        private const int Turn = 360; //360 degrees equal to a turn
        private const double Pi = 180.0; //PI equals to 180 degrees

        #region Properties

        [StringLength(50)]
        public string Name { get; set; }

        public double AngularSpeed { get; set; }

        public int StarDistance { get; set; }

        #endregion

        #region Methods

        public CoordinatesHelper CurrentCoordinates(int day, int nFractionalDigits)
        {
            double angle = ((AngularSpeed * day) % Turn) * Math.PI / Pi;

            var y = Math.Round(Math.Sin(angle) * StarDistance, nFractionalDigits);
            var x = Math.Round(Math.Cos(angle) * StarDistance, nFractionalDigits);

            return new CoordinatesHelper { PositionX = x, PositionY = y };
        }

        public CoordinatesHelper CurrentCoordinates(int day)
        {
            double angle = ((AngularSpeed * day) % Turn) * Math.PI / Pi;

            var y = Math.Sin(angle) * StarDistance;
            var x = Math.Cos(angle) * StarDistance;

            return new CoordinatesHelper { PositionX = x, PositionY = y };
        }

        #endregion
    }
}
