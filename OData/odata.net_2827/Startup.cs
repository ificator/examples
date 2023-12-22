using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Routing.Conventions;
using Owin;
using System.Web.Http;

namespace odata.net_2827
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<TestEntity>("test");

            httpConfiguration.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: null,
                model: modelBuilder.GetEdmModel(),
                pathHandler: new TestODataPathHandler(),
                routingConventions: ODataRoutingConventions.CreateDefaultWithAttributeRouting("odata", httpConfiguration));

            app.UseWebApi(httpConfiguration);
        }
    }
}
