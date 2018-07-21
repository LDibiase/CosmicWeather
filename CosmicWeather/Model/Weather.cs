using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicWeather.Model
{
    public class Weather
    {
        #region Properties

        //Required by EF
        public int ID { get; set; }

        public WeatherEnum WeatherType { get; set; }

        public int DayNumber { get; set; }

        #endregion
    }
}
