using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CosmicWeather.Database;
using CosmicWeather.Model;

namespace CosmicWeatherAPI.Controllers
{
    public class WeathersController : ApiController
    {
        private CosmicWeatherDbContext db = new CosmicWeatherDbContext();

        // GET: api/Weathers
        public IQueryable<Weather> GetWeathers()
        {
            return db.Weathers;
        }

        // GET: api/Weathers/5
        [ResponseType(typeof(Weather))]
        public async Task<IHttpActionResult> GetWeather(int id)
        {
            Weather weather = await db.Weathers.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }

            return Ok(weather);
        }

        // PUT: api/Weathers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeather(int id, Weather weather)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != weather.ID)
            {
                return BadRequest();
            }

            db.Entry(weather).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Weathers
        [ResponseType(typeof(Weather))]
        public async Task<IHttpActionResult> PostWeather(Weather weather)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Weathers.Add(weather);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = weather.ID }, weather);
        }

        // DELETE: api/Weathers/5
        [ResponseType(typeof(Weather))]
        public async Task<IHttpActionResult> DeleteWeather(int id)
        {
            Weather weather = await db.Weathers.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }

            db.Weathers.Remove(weather);
            await db.SaveChangesAsync();

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