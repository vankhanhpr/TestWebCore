using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebCore.Model;

namespace TestWebCore.Data
{
    public class ToDoContext:DbContext
    {
       
        public ToDoContext(DbContextOptions<ToDoContext> options): base(options)
        {
        }
        public  DbSet<TravelSchedule> TravelSchedules { get; set; }
        
    }
}
