using Microsoft.Owin.Hosting;
using System;
using System.IO;
using System.Net;

namespace webapi_autoexpand
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                MakeRequest("");
                MakeRequest("?$expand=e1s");
                MakeRequest("?$expand=e2s");
                MakeRequest("?$expand=e2s($expand=e3s)");
                MakeRequest("?$select=id");
                MakeRequest("?$select=id&$expand=e1s");
                MakeRequest("?$select=id&$expand=e2s");
                MakeRequest("?$select=id&$expand=e2s($expand=e3s)");

                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();
            }
        }

        private static void MakeRequest(string theRest)
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://localhost:12345/root" + theRest);
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
                    Console.WriteLine("Received {0} for '{1}'", response.StatusCode, theRest);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        using (StreamReader responseReader =  new StreamReader(responseStream))
                        {
                            Console.WriteLine($"{responseReader.ReadToEnd()}");
                        }

                        Console.WriteLine();
                    }

                    response.Close();
                    response = null;
                }
            }
        }
    }
}
