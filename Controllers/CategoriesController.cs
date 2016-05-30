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
using aghaApi.Models;

namespace aghaApi.Controllers
{
    public class categoriesController : ApiController
    {
        private finalEntities db = new finalEntities();

        // GET api/categories
       public IQueryable<category> Get()
        {
            return db.categories;
        }

        [HttpGet]
        [ResponseType(typeof(worker_Portfolio))]
        public IHttpActionResult getAvailableWorkers(int sid, string slot)
       {
           List<Availability_Slots> list = db.Availability_Slots.Where(x=> x.Availability_Slots1.Equals(slot) && x.IsAvailable == 0).ToList();
           List<worker_Portfolio> li = new List<worker_Portfolio>();
            foreach(Availability_Slots slots in list)
            {
                if(slots.worker_Portfolio.sid==sid)
                {
                    li.Add(slots.worker_Portfolio);
                }
            }
           return Ok(li);
       }
        [HttpGet]
        [ResponseType(typeof(string))]
       public IHttpActionResult verify(string username, string pass)
       {
           List<user> list = db.users.ToList();
            foreach(user u in list)
            {
                if(u.username.Equals(username) && u.password.Equals(pass))
                {
                    return Ok("Success");
                }
            }
           return Ok("failure");
       }

        // GET api/categories/5
        [ResponseType(typeof(category))]
        public IHttpActionResult Getcategory(int id)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT api/categories/5
        public IHttpActionResult Putcategory(int id, category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoryExists(id))
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

        // POST api/categories
        [ResponseType(typeof(category))]
        public IHttpActionResult Postcategory(category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE api/categories/5
        [ResponseType(typeof(category))]
        public IHttpActionResult Deletecategory(int id)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool categoryExists(int id)
        {
            return db.categories.Count(e => e.Id == id) > 0;
        }
    }
}