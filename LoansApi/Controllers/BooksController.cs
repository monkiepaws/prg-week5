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
    public class BooksController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Books
        public IQueryable<BOOK> GetBOOKs()
        {
            return db.BOOKs;
        }

        // GET: api/Books/5
        [ResponseType(typeof(BOOK))]
        public async Task<IHttpActionResult> GetBOOK(string id)
        {
            BOOK bOOK = await db.BOOKs.FindAsync(id);
            if (bOOK == null)
            {
                return NotFound();
            }

            return Ok(bOOK);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBOOK(string id, BOOK bOOK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bOOK.ISBN)
            {
                return BadRequest();
            }

            db.Entry(bOOK).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BOOKExists(id))
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

        // POST: api/Books
        [ResponseType(typeof(BOOK))]
        public async Task<IHttpActionResult> PostBOOK(BOOK bOOK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BOOKs.Add(bOOK);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BOOKExists(bOOK.ISBN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bOOK.ISBN }, bOOK);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(BOOK))]
        public async Task<IHttpActionResult> DeleteBOOK(string id)
        {
            BOOK bOOK = await db.BOOKs.FindAsync(id);
            if (bOOK == null)
            {
                return NotFound();
            }

            db.BOOKs.Remove(bOOK);
            await db.SaveChangesAsync();

            return Ok(bOOK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BOOKExists(string id)
        {
            return db.BOOKs.Count(e => e.ISBN == id) > 0;
        }
    }
}