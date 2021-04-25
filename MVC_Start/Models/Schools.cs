using System;
using System.Collections.Generic;

namespace MVC_Start.Models
{
    public class Schools
    {
        public Metadata metadata { get; set; }
        public School[] results { get; set; }
    
    }

    public class Metadata
    {
        public int page { get; set; }
        public int total { get; set; }
        public int per_page { get; set; }
    }

    public class School
    {
        public string schoolname { get; set; }
        public string schoolcity { get; set; }
        public string schoolstate { get; set; }
        public string schoolzip { get; set; }
        public int id { get; set; }
        public string ope6_id { get; set; }
    }

  

}
