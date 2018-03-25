using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestCodeBack.Models;
using System.Web.Http.Cors;


namespace TestCodeBack.Controllers
{
	[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
	public class BicyclettesController : ApiController
    {
        private modelEntities db = new modelEntities();

        // GET: api/Bicyclettes
        public IQueryable<Bicyclette> GetBicyclette()
        {
            return db.Bicyclette;
        }

 

        // PUT: api/Bicyclettes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBicyclette(int id, Bicyclette bicyclette)
        {


            if (id != bicyclette.bicycletteId)
            {
                return BadRequest();
            }

            db.Entry(bicyclette).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BicycletteExists(id))
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

        // POST: api/Bicyclettes
        [ResponseType(typeof(Bicyclette))]
        public IHttpActionResult PostBicyclette(Bicyclette bicyclette)
        {
       

            db.Bicyclette.Add(bicyclette);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BicycletteExists(bicyclette.bicycletteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bicyclette.bicycletteId }, bicyclette);
        }

        // DELETE: api/Bicyclettes/5
        [ResponseType(typeof(Bicyclette))]
        public IHttpActionResult DeleteBicyclette(int id)
        {
            Bicyclette bicyclette = db.Bicyclette.Find(id);
            if (bicyclette == null)
            {
                return NotFound();
            }

            db.Bicyclette.Remove(bicyclette);
            db.SaveChanges();

            return Ok(bicyclette);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BicycletteExists(int id)
        {
            return db.Bicyclette.Count(e => e.bicycletteId == id) > 0;
        }
    }
}