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
using MyAmbu.Dto;
using AutoMapper;
namespace MyAmbu.Controllers
{
    public class UbicacionAmbulanciasController : ApiController
    {
        private MyAmbuEntity db = new MyAmbuEntity();

        // GET: api/UbicacionAmbulancias
        public IQueryable<UbicacionAmbulancias> GetUbicacionAmbulancias()
        {
            return db.UbicacionAmbulancias;
        }

        // GET: api/UbicacionAmbulancias/5
        [ResponseType(typeof(UbicacionAmbulancias))]
        public IHttpActionResult GetUbicacionAmbulancias(String id)
        {
            UbicacionAmbulancias ubicacionAmbulancias = db.UbicacionAmbulancias.Where(t=>t.Cedula==id)
                .OrderByDescending(k=>k.Fecha).FirstOrDefault();

            if (ubicacionAmbulancias == null)
            {
                return NotFound();
            }

            return Ok(ubicacionAmbulancias);
        }

        // PUT: api/UbicacionAmbulancias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUbicacionAmbulancias(int id, UbicacionAmbulancias ubicacionAmbulancias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ubicacionAmbulancias.UbicacionAmbulancia)
            {
                return BadRequest();
            }

            db.Entry(ubicacionAmbulancias).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UbicacionAmbulanciasExists(id))
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

        // POST: api/UbicacionAmbulancias
        [ResponseType(typeof(UbicacionAmbulanciasDto))]
        public String PostUbicacionAmbulancias(UbicacionAmbulanciasDto ubicacionAmbulancias)
        {
            if (!ModelState.IsValid)
            {
                return "Error";
            }
            try {
                Mapper.CreateMap<UbicacionAmbulanciasDto, UbicacionAmbulancias>();

                ubicacionAmbulancias.Fecha = DateTime.Now;
                UbicacionAmbulancias Ub = Mapper.Map<UbicacionAmbulancias>(ubicacionAmbulancias);
                db.UbicacionAmbulancias.Add(Ub);
                db.SaveChanges();
                return "Ok";
            } catch (Exception e) {
                return "Error";
            }
            

            
        }

        // DELETE: api/UbicacionAmbulancias/5
        [ResponseType(typeof(UbicacionAmbulancias))]
        public IHttpActionResult DeleteUbicacionAmbulancias(int id)
        {
            UbicacionAmbulancias ubicacionAmbulancias = db.UbicacionAmbulancias.Find(id);
            if (ubicacionAmbulancias == null)
            {
                return NotFound();
            }

            db.UbicacionAmbulancias.Remove(ubicacionAmbulancias);
            db.SaveChanges();

            return Ok(ubicacionAmbulancias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UbicacionAmbulanciasExists(int id)
        {
            return db.UbicacionAmbulancias.Count(e => e.UbicacionAmbulancia == id) > 0;
        }
    }
}