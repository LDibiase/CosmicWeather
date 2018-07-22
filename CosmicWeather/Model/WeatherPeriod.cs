namespace CosmicWeather.Model
{
    public class WeatherPeriod
    {
        #region Properties

        //Required by EF
        public int ID { get; set; }

        public WeatherEnum WeatherType { get; set; }

        public int AmountPeriods { get; set; }

        #endregion
    }
}
