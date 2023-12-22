using Microsoft.Owin.Hosting;
using System;
using System.Net;

namespace odata.net_2827
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                MakeRequest("id");
                MakeRequest("folder(1)");
                MakeRequest("file(1).txt");

                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();
            }
        }

        private static void MakeRequest(string key)
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://localhost:12345/test/" + key);
            HttpWebResponse response = null;

            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            finally
            {
                if (response != null)
                {
                    Console.WriteLine("Received {0} for '{1}'", response.StatusCode, key);

                    response.Close();
                    response = null;
                }
            }
        }
    }
}
