using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MCWebservices.Controllers
{
    public class TeacherController : ApiController
    {
        private SensieLocatorDatabaseEntities2 db = new SensieLocatorDatabaseEntities2();

        public TeacherController()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            //db.Configuration.LazyLoadingEnabled = false;
        }
        // GET api/Teacher
        public IEnumerable<Object> GetTeachers()
        {
            //var lst1 = db.Teachers.ToList();
            var lst2 = db.Requests.ToList();
            ////var req = from a in lst1 join b in lst2 on a.Id equals b.ReceiverId select new {a.Name , b.DateTime , b.SenderId};
            ////var q 
            ////return db.Teachers.AsEnumerable();
            var req = from a in lst2
                      where a.ReceiverId.Contains("1")

                      select new { a.SenderId, a.Student.Name };

            //var sendersList = db.Requests.Where(x => x.ReceiverId == "1").Select(x => x.SenderId).ToList();

            //var studentsList = db.Students.Where(x => sendersList.Any(id => id == x.RollNumber)).ToList();

            //return studentsList;

            //foreach (var item in allRequests)
            //{
                
            //}

            return req.AsEnumerable();
        }

        // GET api/Teacher/5
        public Teacher GetTeacher(string id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return teacher;
        }

        // PUT api/Teacher/5
        public HttpResponseMessage PutTeacher(string id, Teacher teacher)
        {
            if (ModelState.IsValid && id == teacher.Id)
            {
                db.Entry(teacher).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Teacher
        public HttpResponseMessage PostTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, teacher);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = teacher.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Teacher/5
        public HttpResponseMessage DeleteTeacher(string id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Teachers.Remove(teacher);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, teacher);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}