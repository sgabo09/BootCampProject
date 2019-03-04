using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using TodoService.Models;


namespace TodoService.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        private bool isHeader, isBody, isQuery;

        public LoggingFilter(bool isHeader = false, bool isBody = false, bool isQuery = false)
        {
            this.isHeader = isHeader;
            this.isBody = isBody;
            this.isQuery = isQuery;
        }

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            string bodyContent = null, queryString = null, header = null;

            if (isHeader)
            {
                header = filterContext.Request.Headers.ToString();
            }

            if (isBody)
            {    
                using (var stream = new StreamReader(filterContext.Request.Content.ReadAsStreamAsync().Result))
                {
                    stream.BaseStream.Position = 0;
                    bodyContent = stream.ReadToEnd();
                }
            }

            if (isQuery)
            {
                queryString = JsonConvert.SerializeObject(filterContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value));
            }

            var logData = new Log()
            {
                Id = Guid.NewGuid(),
                Method = filterContext.Request.Method.Method,
                Uri = filterContext.Request.RequestUri.ToString(),
                RequestTime = DateTime.Now,
                Header = header,
                Body = bodyContent,
                Query = queryString
            };

            using (var db = new TodoContext())
            {
                db.Logs.Add(logData);
                db.SaveChanges();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}