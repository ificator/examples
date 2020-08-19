using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;

using ServiceLifetime = Microsoft.OData.ServiceLifetime;

namespace ODataAnnotations
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();
            services.AddMvc(setup => { setup.EnableEndpointRouting = false; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            IEdmModel model = GetEdmModel(app.ApplicationServices);

            app.UseMvc(
                routeBuilder =>
                {
                    routeBuilder.Filter().Expand().Select().Count().OrderBy().MaxTop(null);
                    routeBuilder.MapODataServiceRoute(
                        "odata",
                        "odata",
                        containerBuilder =>
                        {
                            containerBuilder
                                    .AddService<IEdmModel>(
                                        ServiceLifetime.Singleton,
                                        sp => model)
                                    .AddService<IEnumerable<IODataRoutingConvention>>(
                                        ServiceLifetime.Singleton,
                                        sp => ODataRoutingConventions.CreateDefaultWithAttributeRouting("odata", routeBuilder))
                                    .AddService<ODataSerializerProvider>(
                                        ServiceLifetime.Singleton,
                                        sp => new SimpleSerializerProvider(sp));
                        });
                });
        }

        private static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            builder.EntitySet<Simple>("Data").EntityType.Ignore(s => s.Annotations);
            return builder.GetEdmModel();
        }
    }
}
