using System;

namespace MVC_Start.Models
{

    public class Programs
    {
        public Metadata2 metadata2 { get; set; }
        public Result[] results { get; set; }
    }

    public class Metadata2
    {
        public int page { get; set; }
        public int total { get; set; }
        public int per_page { get; set; }
    }

    public class Result
    {
        public Program[] data { get; set; }
    }

    public class Program
    {
        public string code { get; set; }
        public string title { get; set; }
        public string ope6_id { get; set; }
        public int unit_id { get; set; }
    }


}
