using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestWebCore.Model;
using WebTravelApplication.Helper;
using WebTravelApplication.Models;

namespace WebTravelApplication.Controllers
{
    public class HomeController : Controller
    {
        public async Task<HttpResponseMessage> SendRequestAsyncSchedule([FromBody]TravelSchedule item)
        {
            Data data = new Data();
            using (HttpClient httpClient = new HttpClient())
            {
                var schedule = await Task.Run(() => JsonConvert.SerializeObject(item));
                var httpContent = new StringContent(schedule, Encoding.UTF8, "application/json");
                

                HttpClient client = data.Initial();
                HttpResponseMessage responseMessage = null;
                try
                {
                    responseMessage = await client.PostAsync("api/travelschedule/insertschedule", httpContent);
                }
                catch (Exception ex)
                {
                    if (responseMessage == null)
                    {
                        responseMessage = new HttpResponseMessage();
                    }
                    responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                    responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
                }
                return responseMessage;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
