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
    public class LoansController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Loans
        public IQueryable<LOAN> GetLOANs()
        {
            return db.LOANs;
        }

        // GET: api/Loans/5
        [ResponseType(typeof(LOAN))]
        public async Task<IHttpActionResult> GetLOAN(string id)
        {
            LOAN lOAN = await db.LOANs.FindAsync(id);
            if (lOAN == null)
            {
                return NotFound();
            }

            return Ok(lOAN);
        }

        // PUT: api/Loans/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLOAN(string id, LOAN lOAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lOAN.StudentId)
            {
                return BadRequest();
            }

            db.Entry(lOAN).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LOANExists(id))
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

        // POST: api/Loans
        [ResponseType(typeof(LOAN))]
        public async Task<IHttpActionResult> PostLOAN(LOAN lOAN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LOANs.Add(lOAN);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LOANExists(lOAN.StudentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lOAN.StudentId }, lOAN);
        }

        // DELETE: api/Loans/5
        [ResponseType(typeof(LOAN))]
        public async Task<IHttpActionResult> DeleteLOAN(string id)
        {
            LOAN lOAN = await db.LOANs.FindAsync(id);
            if (lOAN == null)
            {
                return NotFound();
            }

            db.LOANs.Remove(lOAN);
            await db.SaveChangesAsync();

            return Ok(lOAN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LOANExists(string id)
        {
            return db.LOANs.Count(e => e.StudentId == id) > 0;
        }
    }
}