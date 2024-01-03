using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Routing.Conventions;
using Owin;
using System;
using System.Web.Http;

namespace webapi_autoexpand
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            httpConfiguration.SetDefaultQuerySettings(
                new DefaultQuerySettings
                {
                    EnableExpand = true,
                    EnableSelect = true,
                });

            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Root>("root");

            httpConfiguration.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: null,
                model: modelBuilder.GetEdmModel(),
                pathHandler: new DefaultODataPathHandler(),
                routingConventions: ODataRoutingConventions.CreateDefaultWithAttributeRouting("odata", httpConfiguration));

            app.UseWebApi(httpConfiguration);
        }
    }
}
