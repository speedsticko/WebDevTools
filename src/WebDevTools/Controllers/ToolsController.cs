using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

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

        public IActionResult ServerSniffer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ServerSniffer(string uri)
        {
            var response_headers = new Dictionary<string, string>();
            using (var http_client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Head, uri);
                using (var response = await http_client.SendAsync(request))
                {
                    foreach(var header in response.Headers)
                    {
                        response_headers.Add(header.Key, string.Join(",", header.Value));
                    }
                }
            }
            var json_result = new JsonResult(response_headers);
            return json_result;
        }
    }
}
