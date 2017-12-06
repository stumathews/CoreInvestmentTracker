using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Http;
//using System.Web.Http.OData;
//using Microsoft.Practices.Unity;
using CoreInvestmentTracker.Common.ActionFilters.WebApi;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Common
{
    [GlobalLoggingWebApi]
    public class EntityManagedODataController<T> : ODataController where T : class, IDbInvestmentEntity
    {
        /// <summary>
        /// Access to te underlying store of entities
        /// </summary>
        [Dependency]
        public IEntityApplicationDbContext<T> EntityRepository { get; set; }

        // PUT: odata/Risks(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<T> patch)
        {
            Console.WriteLine($"Put {key}");
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            T T = await EntityRepository.Entities.FindAsync(key);
            if (T == null)
            {
                return NotFound();
            }

            patch.Put(T);

            try
            {
                await EntityRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(T);
        }

        // POST: odata/Risks
        public async Task<IHttpActionResult> Post(T T)
        {
            Console.WriteLine($"Post {T.Name}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EntityRepository.Entities.Add(T);
            await EntityRepository.SaveChangesAsync();

            return Created(T);
        }

        
        // PATCH: odata/Risks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<T> patch)
        {
            
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            T T = await EntityRepository.Entities.FindAsync(key);
            if (T == null)
            {
                return NotFound();
            }

            patch.Patch(T);

            try
            {
                await EntityRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(T);
        }

        // DELETE: odata/Risks(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            T T = await EntityRepository.Entities.FindAsync(key);
            if (T == null)
            {
                return NotFound();
            }

            EntityRepository.Entities.Remove(T);
            await EntityRepository.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool Exists(int key)
        {
            return EntityRepository.Entities.Count(e => e.ID == key) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EntityRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}