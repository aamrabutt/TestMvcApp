using MCWebservices.Models;
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
    public class StudentController : ApiController
    {
        private SensieLocatorDatabaseEntities2 db = new SensieLocatorDatabaseEntities2();

        public StudentController()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            //db.Configuration.LazyLoadingEnabled = false;
        }
        // GET api/Student
        public IEnumerable<StudentModel> GetStudents()
        {
            var dbList = db.Students.ToList();

            List<StudentModel> toReturn = new List<StudentModel>();
            foreach (var item in dbList)
            {
                StudentModel model = new StudentModel();
                model.Email = item.Email;
                model.RollNumber = item.RollNumber;
                model.Name = item.Name;
                model.Password = item.Password;

                toReturn.Add(model);
            }

            return toReturn;
        }

        // GET api/Student/5
        public Student GetStudent(string id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return student;
        }

        // PUT api/Student/5
        public HttpResponseMessage PutStudent(string id, Student student)
        {
            if (ModelState.IsValid && id == student.RollNumber)
            {
                db.Entry(student).State = EntityState.Modified;

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

        // POST api/Student
        public HttpResponseMessage PostStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, student);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = student.RollNumber }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Student/5
        public HttpResponseMessage DeleteStudent(string id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Students.Remove(student);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}