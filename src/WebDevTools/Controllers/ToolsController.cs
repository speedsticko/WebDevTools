using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using HtmlAgilityPack;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevTools.Controllers
{
    public class ToolsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        // -- Http server sniffer
        // --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---

        class HttpHeaders
        {
            public Dictionary<string, string> RequestHeaders { get; set; }
            public Dictionary<string, string> ResponseHeaders { get; set; }
        }

        public IActionResult ServerSniffer()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ServerSniffer(string uri)
        {

            var request_headers = new Dictionary<string, string>();
            var response_headers = new Dictionary<string, string>();

            var http_headers = new HttpHeaders();
            http_headers.RequestHeaders = request_headers;
            http_headers.ResponseHeaders = response_headers;
            using (var http_client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Head, uri);
                using (var response = await http_client.SendAsync(request))
                {
                    foreach (var header in response.Headers)
                    {
                        response_headers.Add(header.Key, string.Join(",", header.Value));
                    }
                    foreach (var header in request.Headers)
                    {
                        request_headers.Add(header.Key, string.Join(",", header.Value));
                    }
                }
            }
            var json_result = new JsonResult(http_headers);
            return json_result;
        }

        // --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---
        // -- Page Built With
        // --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---

        class BuiltWithInfo
        {
            public List<string> Scripts = new List<string>();
            public List<string> ThirdPartyLibraries = new List<string>();
            public List<string> Stylesheets = new List<string>();
        }

        public IActionResult BuiltWith()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuiltWith(string uri)
        {

            var info = new BuiltWithInfo();
            using (var http_client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                using (var response = await http_client.SendAsync(request))
                {
                    HtmlDocument doc = new HtmlDocument();
               
                }
            }
            var json_result = new JsonResult(info);
            return json_result;
        }

    }
}
