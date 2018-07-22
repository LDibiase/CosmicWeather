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
    public class WeathersController : ApiController
    {
        private CosmicWeatherDbContext db = new CosmicWeatherDbContext();

        private static readonly Expression<Func<Weather, WeatherDto>> AsWeatherDto =
            x => new WeatherDto
            {
                Dia = x.DayNumber,
                Clima = x.WeatherType.ToString(),
            };


        // GET: api/Weathers
        public IQueryable<WeatherDto> GetWeathers()
        {
            return db.Weathers.Select(AsWeatherDto);
        }

        // GET: api/Weathers/5
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