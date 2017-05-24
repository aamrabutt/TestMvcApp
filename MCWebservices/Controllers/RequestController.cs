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
    public class RequestController : ApiController
    {
        private SensieLocatorDatabaseEntities2 db = new SensieLocatorDatabaseEntities2();

        // GET api/Request
         public RequestController()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            //db.Configuration.LazyLoadingEnabled = false;

            

        }
        public IEnumerable<Request> GetRequests()
        {
           // var requests = db.Requests.Include(r => r.Student).Include(r => r.Teacher);
            var requests = db.Requests.ToList();
            return requests.AsEnumerable();
        }

        // GET api/Request/5
        public Request GetRequest(int id)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return request;
        }

        // PUT api/Request/5
        public HttpResponseMessage PutRequest(int id, Request request)
        {
            if (ModelState.IsValid && id == request.Id)
            {
                db.Entry(request).State = EntityState.Modified;

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

        // POST api/Request
        public HttpResponseMessage PostRequest(Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, request);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = request.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Request/5
        public HttpResponseMessage DeleteRequest(int id)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Requests.Remove(request);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, request);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}