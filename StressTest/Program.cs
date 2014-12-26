using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRequest();
        }

        private static void TestRequest()
        {
            var cookieContainer = new System.Net.CookieContainer();
            while (true)
            {
                var client = (HttpWebRequest)WebRequest.Create("http://localhost:57762/");
                client.Timeout = 1000000;
                client.CookieContainer = cookieContainer;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var response = client.GetResponse();
                var responseStream = response.GetResponseStream();
                
                MemoryStream stream = new MemoryStream();
                responseStream.CopyTo(stream);
                sw.Stop();
                Console.WriteLine("Length: {0}, Time: {1}", stream.Length, sw.Elapsed);
            }
        }
    }
}
