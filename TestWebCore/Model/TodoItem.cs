using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebCore.Model
{
    public class TodoItem
    {
        [Key]

        public int ID { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public string Departure { get; set; }

        public string Duration { get; set; }

        public DateTime DateStart { get; set; }

        public int Daytotal { get; set; }

        public float Cost { get; set; }
    }
}
