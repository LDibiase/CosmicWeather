using System.ComponentModel.DataAnnotations;

namespace CosmicWeather.Model
{
    public class Planet
    {
        #region Properties

        [StringLength(50)]
        public string Name { get; set; }

        public decimal AngularSpeed { get; set; }

        public decimal InitialAngle { get; set; }

        public int StarDistance { get; set; }

        #endregion

        #region Methods

        public decimal CurrentAngle(int day)
        {
            return 0;
        }

        #endregion
    }
}
