using System.Net;
using System.Net.Http;
using System.Web.Http;
using BootCampProject.Controllers.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TodoTest
{
    [TestClass]
    public class TestHealthController
    {
        [TestMethod]
        public void GetHealthCheckStatus()
        {
            var controller = new HealthController
            {
                Request = new HttpRequestMessage(), Configuration = new HttpConfiguration()
            };

            var status = controller.GetHealth().StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, status);
        }
    }
}