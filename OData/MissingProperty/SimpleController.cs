using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace ODataAnnotations
{
    public class SimpleController : ODataController
    {
        [HttpGet]
        [ODataRoute("/Data")]
        [EnableQuery]
        public IActionResult Get()
        {
            bool excludeComplex = this.Request.Query["repro"] == "1";

            return this.Ok(
                new Simple[]
                {
                    new Simple
                    {
                        Complex = excludeComplex ? null : new Complex { Value = "c1" },
                        Id = "1",
                        Name = "One",
                    },
                    new Simple
                    {
                        Complex = excludeComplex ? null : new Complex { Value = "c2" },
                        Id = "2",
                        Name = "Two",
                    },
                    new Simple
                    {
                        Complex = excludeComplex ? null : new Complex { Value = "c3" },
                        Id = "3",
                        Name = "Three",
                    },
                });
        }
    }
}
