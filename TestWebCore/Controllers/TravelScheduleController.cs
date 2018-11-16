using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using TestWebCore.Data;
using TestWebCore.Model;

namespace TestWebCore.Controllers
{
    [Route("api/travelschedule")]
    [ApiController]
    public class TravelScheduleController : Controller
    {
        private readonly ToDoContext context;
        private readonly IHostingEnvironment env;

        public TravelScheduleController(ToDoContext context, IHostingEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        [HttpGet("getschedule")]
        public List<TravelSchedule> GetAll()
        {
            return context.TravelSchedules.ToList();
        }

        [HttpPut("updateshedule")]
        public IActionResult Update([FromBody]TravelSchedule item)
        {
            var todo = context.TravelSchedules.Find(item.ID);
            if (todo == null)
            {
                return NotFound();
            }
            todo.ID = item.ID;
            todo.Name = item.Name;
            todo.Duration = todo.Duration;
            todo.Describe = item.Describe;
            todo.Daytotal = item.Daytotal;
            todo.DateStart = item.DateStart;
            todo.Cost = item.Cost;
            todo.Departure = item.Departure;
            context.TravelSchedules.Update(todo);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("deleteschedule")]
        public IActionResult Delete([FromBody]int id)
        {
            var todo = context.TravelSchedules.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            context.TravelSchedules.Remove(todo);
            context.SaveChanges();
            return NoContent();
        }


        [HttpPost("insertschedule")]
        public IActionResult Create([FromBody]TravelSchedule postData)
        {
            if (postData == null)
            {
                return BadRequest();
            }
            context.TravelSchedules.Add(postData);
            context.SaveChanges();

            return NoContent();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFiles([FromBody]List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return Content("files not selected");

            foreach (var file in files)
            {
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.Name);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return RedirectToAction("Files");
        }

    }

}