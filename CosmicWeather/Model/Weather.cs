﻿namespace CosmicWeather.Model
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
