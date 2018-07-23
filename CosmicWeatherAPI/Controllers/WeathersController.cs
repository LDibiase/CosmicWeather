using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CosmicWeather.Database;
using CosmicWeather.Model;
using CosmicWeatherAPI.DTOs;

namespace CosmicWeatherAPI.Controllers
{
    [RoutePrefix("api/weathers")]
    public class WeathersController : ApiController
    {
        private CosmicWeatherDbContext db = new CosmicWeatherDbContext();

        private static readonly Expression<Func<Weather, WeatherDto>> AsWeatherDto =
            x => new WeatherDto
            {
                Dia = x.DayNumber,
                Clima = x.WeatherType.ToString(),
            };

        private static readonly Expression<Func<WeatherPeriod, WeatherPeriodDto>> AsWeatherPeriodDto =
            x => new WeatherPeriodDto
            {
                Clima = x.WeatherType.ToString(),
                Periodos = x.AmountPeriods,
            };

        [Route("")]
        [ResponseType(typeof(WeatherDto))]
        public IQueryable<WeatherDto> GetWeathers()
        {
            return db.Weathers.Select(AsWeatherDto);
        }

        [Route("{day:int}")]
        [ResponseType(typeof(WeatherDto))]
        public async Task<IHttpActionResult> GetWeather(int day)
        {
            WeatherDto weather = await db.Weathers
                .Where(b => b.DayNumber == day)
                .Select(AsWeatherDto)
                .FirstOrDefaultAsync();
            if (weather == null)
            {
                return NotFound();
            }

            return Ok(weather);
        }

        [Route("periods/{weatherType}")]
        [ResponseType(typeof(WeatherPeriodDto))]
        public async Task<IHttpActionResult> GetWeatherPeriods(string weatherType)
        {
            int weatherTypeId = (int)Enum.Parse(typeof(WeatherEnum), weatherType.ToLower());

            WeatherPeriodDto weather = await db.WeatherPeriods
                .Where(b => (int)b.WeatherType == weatherTypeId)
                .Select(AsWeatherPeriodDto)
                .FirstOrDefaultAsync();

            if (weather == null)
            {
                return NotFound();
            }

            return Ok(weather);
        }

        [Route("periods/lluvia/max")]
        [ResponseType(typeof(WeatherDto))]
        public async Task<IHttpActionResult> GetMaxRainDay()
        {
            int weatherTypeId = (int)Enum.Parse(typeof(WeatherEnum), "lluviaMaxima");

            WeatherDto weather = await db.Weathers
                .Where(w => (int)w.WeatherType == weatherTypeId)
                .Select(AsWeatherDto)
                .FirstOrDefaultAsync();

            if (weather == null)
            {
                return NotFound();
            }

            return Ok(weather);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WeatherExists(int id)
        {
            return db.Weathers.Count(e => e.ID == id) > 0;
        }
    }
}