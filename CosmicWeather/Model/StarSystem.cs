using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CosmicWeather.Model
{
    public class StarSystem
    {
        #region Properties

        [StringLength(50)]
        public string Name { get; set; }

        public List<Planet> Planets { get; set; }

        public List<Weather> Weathers { get; set; }

        #endregion

        #region Methods

        public WeatherEnum GetDayWeather(int day)
        {
            return WeatherEnum.Lluvia;
        }

        public bool IsRain(List<float> angles)
        {
            return false;
        }

        public bool IsMaxRain(List<float> angles)
        {
            return false;
        }

        public bool IsDry(List<float> angles)
        {
            return false;
        }

        public bool IsOptimum(List<float> angles)
        {
            return false;
        }

        public List<Weather> GetWeatherForDays(int days)
        {
            return new List<Weather>();
        }

        #endregion
    }
}
