using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicWeather.Model
{
    public enum WeatherEnum
    {
        Drought = 1,
        Optimum,
        Rain,
        MaxRain,
        Normal //When weather isn't rain, isn't drought and isn't optimum weather either
    }
}
