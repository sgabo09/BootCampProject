using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BootCampProject.Controllers.API
{
    public class HealthController : ApiController
    {
        //GET /api/health
        public HttpResponseMessage GetHealth()
        {
            return Request.CreateResponse(HttpStatusCode.OK, DateTime.Now);
        }
    }
}
