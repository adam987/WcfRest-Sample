using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace RestClient
{
    internal class Program
    {
        private const string JsonContentType = "application/json";
        private const string XmlContentType = "application/xml";
        private static readonly JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();

        private static void Main(string[] args)
        {
            try
            {
                var request = CreateRequest("");
                request.Accept = JsonContentType;
                using (var response = GetResponse(request))
                {
                    var body = GetBody(response);
                    Console.WriteLine($"Status code: {response.StatusCode} {(int) response.StatusCode}");
                    Console.WriteLine(body);
                }
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse) e.Response;
                Console.WriteLine("Webexception");
                Console.WriteLine($"Status code: {response.StatusCode} {(int) response.StatusCode}");
                Console.WriteLine(GetBody(response));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexcpected exception: {e}");
            }

            Console.ReadKey(true);
        }

        private static HttpWebRequest CreateRequest(string relativePath)
        {
            return WebRequest.CreateHttp($"http://localhost:18964/RestService.svc/{relativePath}");
        }

        private static HttpWebResponse GetResponse(WebRequest request)
        {
            return (HttpWebResponse) request.GetResponse();
        }

        private static string GetBody(WebResponse response)
        {
            try
            {
                var stream = response.GetResponseStream();
                if (stream == null)
                    return null;
                var streamReader = new StreamReader(stream);
                var body = streamReader.ReadToEnd();
                streamReader.Close();
                return body;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        private static void WriteBody(WebRequest request, string body, string contentType, string method)
        {
            try
            {
                request.ContentLength = body.Length;
                request.ContentType = contentType;
                request.Method = method;
                var streamWriter = new StreamWriter(request.GetRequestStream()) {AutoFlush = true};
                streamWriter.Write(body);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static T Deserialize<T>(string body) where T : class
        {
            try
            {
                return JavaScriptSerializer.Deserialize<T>(body);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private static string Serialize<T>(T obj) where T : class
        {
            try
            {
                return JavaScriptSerializer.Serialize(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}