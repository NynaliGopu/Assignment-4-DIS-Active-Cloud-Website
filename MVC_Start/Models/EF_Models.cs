using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Start.Models  

{
    public class SchoolData
    {

        [Key]
        public int id { get; set; }
        public string ope6_id
        {
            get; set;
        }
        
    
   
        public string schoolname { get; set; }
        public string schoolcity { get; set; }
        public string schoolstate { get; set; }
        public string schoolzip { get; set; }
       
        //[ForeignKey("ope6_id")] 
        //public List<ProgramData> programinfo { get; set; }
    }

    public class ProgramData
        
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string code { get; set; }
        public string title { get; set; }
        public string ope6_id { get; set; }
        public int unit_id { get; set; }

        //[ForeignKey("ope6_id")]
        //public List<SchoolData> schools { get; set; }
    }


}