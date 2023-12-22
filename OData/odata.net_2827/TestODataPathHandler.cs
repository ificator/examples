using Microsoft.AspNet.OData.Routing;
using System;

namespace odata.net_2827
{
    public class TestODataPathHandler : DefaultODataPathHandler
    {
        public override ODataPath Parse(string serviceRoot, string odataPath, IServiceProvider requestContainer)
        {
            try
            {
                return base.Parse(serviceRoot, odataPath, requestContainer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tException {0}: {1}", ex.GetType().Name, ex.Message);
                throw;
            }
        }
    }
}
