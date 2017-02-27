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
using MyAmbu.Models;
using AutoMapper;
using MyAmbu.Dto;

namespace MyAmbu.Controllers
{
    public class AmbulanciasController : ApiController
    {
        private MyAmbuEntity db = new MyAmbuEntity();

        // GET: api/Ambulacias
        public List<AmbulanciasDto> GetAmbulacias()
        {

            Mapper.CreateMap<Ambulancias, AmbulanciasDto>();
            return Mapper.Map<List<AmbulanciasDto>>(db.Ambulacias);
            
        }

        // GET: api/Ambulacias/5
        [ResponseType(typeof(Ambulancias))]
        public IHttpActionResult GetAmbulacias(int id)
        {
            Ambulancias ambulacias = db.Ambulacias.Find(id);
            if (ambulacias == null)
            {
                return NotFound();
            }

            return Ok(ambulacias);
        }

        // PUT: api/Ambulacias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAmbulacias(int id, Ambulancias ambulacias)
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
        [ResponseType(typeof(Ambulancias))]
        public IHttpActionResult PostAmbulacias(Ambulancias ambulacias)
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
        [ResponseType(typeof(Ambulancias))]
        public IHttpActionResult DeleteAmbulacias(int id)
        {
            Ambulancias ambulacias = db.Ambulacias.Find(id);
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