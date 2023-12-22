using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System;
using System.Web.Http;

namespace odata.net_2827
{
    public class TestController : ODataController
    {   
        [HttpGet]
        [ODataRoute("/test({key})")]
        public IHttpActionResult GetTestById(string key)
        {
            Console.WriteLine("Received request for '{0}'", key);
            return this.Ok();
        }
    }
}
