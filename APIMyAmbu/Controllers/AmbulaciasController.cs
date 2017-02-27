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
using APIMyAmbu.Models;

namespace APIMyAmbu.Controllers
{
    public class AmbulaciasController : ApiController
    {
        private MyAmbu db = new MyAmbu();

        // GET: api/Ambulacias
        public IQueryable<Ambulacias> GetAmbulacias()
        {
            return db.Ambulacias;
        }

        // GET: api/Ambulacias/5
        [ResponseType(typeof(Ambulacias))]
        public IHttpActionResult GetAmbulacias(int id)
        {
            Ambulacias ambulacias = db.Ambulacias.Find(id);
            if (ambulacias == null)
            {
                return NotFound();
            }

            return Ok(ambulacias);
        }

        // PUT: api/Ambulacias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAmbulacias(int id, Ambulacias ambulacias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ambulacias.IdAmbulancia)
            {
                return BadRequest();
            }

            db.Entry(ambulacias).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmbulaciasExists(id))
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

        // POST: api/Ambulacias
        [ResponseType(typeof(Ambulacias))]
        public IHttpActionResult PostAmbulacias(Ambulacias ambulacias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ambulacias.Add(ambulacias);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ambulacias.IdAmbulancia }, ambulacias);
        }

        // DELETE: api/Ambulacias/5
        [ResponseType(typeof(Ambulacias))]
        public IHttpActionResult DeleteAmbulacias(int id)
        {
            Ambulacias ambulacias = db.Ambulacias.Find(id);
            if (ambulacias == null)
            {
                return NotFound();
            }

            db.Ambulacias.Remove(ambulacias);
            db.SaveChanges();

            return Ok(ambulacias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AmbulaciasExists(int id)
        {
            return db.Ambulacias.Count(e => e.IdAmbulancia == id) > 0;
        }
    }
}