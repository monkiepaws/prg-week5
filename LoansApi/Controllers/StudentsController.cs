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
using LoansApi.Models;

namespace LoansApi.Controllers
{
    public class StudentsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Students
        public IQueryable<STUDENT> GetSTUDENTs()
        {
            return db.STUDENTs;
        }

        // GET: api/Students/5
        [ResponseType(typeof(STUDENT))]
        public async Task<IHttpActionResult> GetSTUDENT(string id)
        {
            STUDENT sTUDENT = await db.STUDENTs.FindAsync(id);
            if (sTUDENT == null)
            {
                return NotFound();
            }

            return Ok(sTUDENT);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSTUDENT(string id, STUDENT sTUDENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sTUDENT.StudentId)
            {
                return BadRequest();
            }

            db.Entry(sTUDENT).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!STUDENTExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(STUDENT))]
        public async Task<IHttpActionResult> PostSTUDENT(STUDENT sTUDENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.STUDENTs.Add(sTUDENT);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (STUDENTExists(sTUDENT.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sTUDENT.StudentId }, sTUDENT);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(STUDENT))]
        public async Task<IHttpActionResult> DeleteSTUDENT(string id)
        {
            STUDENT sTUDENT = await db.STUDENTs.FindAsync(id);
            if (sTUDENT == null)
            {
                return NotFound();
            }

            db.STUDENTs.Remove(sTUDENT);
            await db.SaveChangesAsync();

            return Ok(sTUDENT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool STUDENTExists(string id)
        {
            return db.STUDENTs.Count(e => e.StudentId == id) > 0;
        }
    }
}