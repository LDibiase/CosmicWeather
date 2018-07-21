using System.ComponentModel.DataAnnotations;

namespace CosmicWeather.Model
{
    public class Planet
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
