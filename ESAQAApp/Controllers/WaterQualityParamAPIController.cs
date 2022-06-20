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
using Microsoft.AspNet.Identity;
using DOF003.Models;

namespace DOF003.Controllers
{
    public class WaterQualityParamAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/WaterQualityParamAPI
        public IQueryable<WaterQualityParam> GetWaterQualityParams()
        {
            return db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false).Include(i => i.WaterQualityCategory);
        }

        // GET: api/WaterQualityParamAPI/5
        [ResponseType(typeof(WaterQualityParam))]
        public async Task<IHttpActionResult> GetWaterQualityParam(int id)
        {
            WaterQualityParam waterQualityParam = await db.WaterQualityParams.FindAsync(id);
            if (waterQualityParam == null)
            {
                return NotFound();
            }

            return Ok(waterQualityParam);
        }

        // PUT: api/WaterQualityParamAPI/5
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> PutWaterQualityParam(int id, WaterQualityParam waterQualityParam)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return Ok(false);
            }

            if (id != waterQualityParam.WaterQualityParamID)
            {
                //return BadRequest();
                return Ok(false);
            }

            db.Entry(waterQualityParam).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                return Ok(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaterQualityParamExists(id))
                {
                    //return NotFound();
                    return Ok(false);
                }
                else
                {
                    return Ok(false);
                    //throw;
                }
            }
            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/WaterQualityParamAPI
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> PostWaterQualityParam(WaterQualityParam waterQualityParam)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return Ok(false);
            }

            try
            {
                db.WaterQualityParams.Add(waterQualityParam);
                await db.SaveChangesAsync();
                return Ok(true);
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }

        // DELETE: api/WaterQualityParamAPI/5
        [ResponseType(typeof(WaterQualityParam))]
        public async Task<IHttpActionResult> DeleteWaterQualityParam(int id)
        {
            WaterQualityParam waterQualityParam = await db.WaterQualityParams.FindAsync(id);
            if (waterQualityParam == null)
            {
                return NotFound();
            }

            db.WaterQualityParams.Remove(waterQualityParam);
            await db.SaveChangesAsync();

            return Ok(waterQualityParam);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WaterQualityParamExists(int id)
        {
            return db.WaterQualityParams.Count(e => e.WaterQualityParamID == id) > 0;
        }
    }
}