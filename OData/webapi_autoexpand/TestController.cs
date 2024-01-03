using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using System.Web.Http;

namespace webapi_autoexpand
{
    public class TestController : ODataController
    {
        [HttpGet]
        [ODataRoute("/root")]
        [EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All)]
        public IHttpActionResult GetRoot()
        {
            return this.Ok(
                new Root
                {
                    E1s = new Expandable1[]
                    {
                        new Expandable1
                        {
                            Id = "expandable1.1",
                        },
                    },
                    E2s = new Expandable2[]
                    {
                        new Expandable2
                        {
                            E1s = new Expandable1[]
                            {
                                new Expandable1
                                {
                                    Id = "expandable1.2",
                                },
                            },
                            E3s = new Expandable3[]
                            {
                                new Expandable3
                                {
                                    E1s = new Expandable1[]
                                    {
                                        new Expandable1
                                        {
                                            Id = "expandable1.3",
                                        },
                                    },
                                    Id = "expandable3",
                                },
                            },
                            Id = "expandable2",
                        },
                    },
                    Id = "root",
                });
        }
    }
}
