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
            this.Request.Headers.Add("Prefer", "odata.include-annotations=*");

            return this.Ok(
                new Simple[]
                {
                    new Simple
                    {
                        Annotations =
                        {
                            { "simple.objectannotation", "objectannotation1" },
                            { "Name@simple.propertyannotation", "propertyannotation1" }
                        },
                        Id = "1",
                        Name = "One",
                    },
                    new Simple
                    {
                        Annotations =
                        {
                            { "simple.objectannotation", "objectannotation2" },
                            { "Name@simple.propertyannotation", "propertyannotation2" }
                        },
                        Id = "2",
                        Name = "Two",
                    },
                    new Simple
                    {
                        Annotations =
                        {
                            { "simple.objectannotation", "objectannotation2" },
                            { "Name@simple.propertyannotation", "propertyannotation2" }
                        },
                        Id = "3",
                        Name = "Three",
                    },
                });
        }
    }
}
