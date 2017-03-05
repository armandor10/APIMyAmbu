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

namespace MyAmbu.Controllers
{
    public class ParamedicosController : ApiController
    {
        private MyAmbuEntity db = new MyAmbuEntity();
        private HubAlarma Hub = new HubAlarma();

        [Route("api/ListadoSignalR")]
        public Dictionary<string, string> Get()
        {
            return Hub.Listado();
        }

        // GET: api/Paramedicos
        [Route("api/Paramedicos")]
        public IQueryable<Paramedicos> GetParamedicos()
        {
            return db.Paramedicos;
        }

        // GET: api/Paramedicos/5
        [ResponseType(typeof(Paramedicos))]
        public IHttpActionResult GetParamedicos(string id)
        {
            Paramedicos paramedicos = db.Paramedicos.Find(id);
            if (paramedicos == null)
            {
                return NotFound();
            }

            return Ok(paramedicos);
        }

        // PUT: api/Paramedicos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParamedicos(string id, Paramedicos paramedicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paramedicos.Cedula)
            {
                return BadRequest();
            }

            db.Entry(paramedicos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParamedicosExists(id))
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

        // POST: api/Paramedicos
        [Route("api/Paramedicos")]
        [ResponseType(typeof(Paramedicos))]
        [HttpPost]
        public IHttpActionResult PostParamedicos(Paramedicos paramedicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Paramedicos.Add(paramedicos);
                db.SaveChanges();
                return Ok("Paramedico Guardado");
            }
            catch (DbUpdateException)
            {
                if (ParamedicosExists(paramedicos.Cedula))
                {
                    return BadRequest("El paramedico ya existe");
                }
                else
                {
                    throw;
                }
            }
            //return CreatedAtRoute("DefaultApi", new { id = paramedicos.Cedula }, paramedicos);
        }

        // POST: api/Paramedicos
        [Route("api/LoginParamedicos")]
        [HttpPost]
        [ResponseType(typeof(LoginParamedicoDto))]
        public IHttpActionResult PostParamedicosLogin(LoginParamedicoDto Login)
        {
            Paramedicos medico = db.Paramedicos.Find(Login.Cedula);
            if (medico!=null)
            {
                if (Login.Password == medico.Password)
                {
                    return Ok(medico);
                }
                else {
                    return NotFound();

                }
            }
            return NotFound();
        }
        // POST: api/Paramedicos
        [Route("api/LogoutParamedicos")]
        [HttpPost]
        [ResponseType(typeof(LoginParamedicoDto))]
        public IHttpActionResult PostParamedicosLogout(LoginParamedicoDto Login)
        {
            Paramedicos medico = db.Paramedicos.Find(Login.Cedula);
            if (medico != null)
            {
                if (Login.Password == medico.Password)
                {
                    return Ok(medico);
                }
                else
                {
                    return NotFound();

                }
            }
            return NotFound();
        }
        // DELETE: api/Paramedicos/5
        [ResponseType(typeof(Paramedicos))]
        public IHttpActionResult DeleteParamedicos(string id)
        {
            Paramedicos paramedicos = db.Paramedicos.Find(id);
            if (paramedicos == null)
            {
                return NotFound();
            }

            db.Paramedicos.Remove(paramedicos);
            db.SaveChanges();

            return Ok(paramedicos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParamedicosExists(string id)
        {
            return db.Paramedicos.Count(e => e.Cedula == id) > 0;
        }
    }
}