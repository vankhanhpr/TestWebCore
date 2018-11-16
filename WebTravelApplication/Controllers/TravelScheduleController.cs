using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestWebCore.Model;
using WebTravelApplication.Helper;

namespace WebTravelApplication.Controllers
{
    public class TravelScheduleController : Controller
    {
        Data data = new Data();
        public async Task<IActionResult> TravelSchedule()
        {
            List<TravelSchedule> listCustomer = new List<TravelSchedule>();
            HttpClient client = data.Initial();
            HttpResponseMessage res = await client.GetAsync("api/travelschedule/getschedule").ConfigureAwait(false); ;

            if (res.IsSuccessStatusCode)
            {
                Console.WriteLine(res.ToString());
                var result = res.Content.ReadAsStringAsync().Result;
                listCustomer = JsonConvert.DeserializeObject<List<TravelSchedule>>(result);
            }

            return View(listCustomer);
        }


        public async Task<HttpResponseMessage> SendRequestAsyncSchedule(TravelSchedule item)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var schedule = await Task.Run(() => JsonConvert.SerializeObject(item));
                var httpContent = new StringContent(schedule, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = null;
                try
                {
                    responseMessage = await httpClient.PostAsync("api/travelschedule/insertschedule", httpContent);
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

        public IActionResult SendRequestAsyncSchedule()
        {
            return View();
        }
    }
}