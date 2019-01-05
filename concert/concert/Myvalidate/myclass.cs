using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace concert.Models
{
    public class myclass
    {
        [Required(ErrorMessage = "กรุณาป้อนวันที่")]
        public DateTime Mydate { get; set; }

        public HttpPostedFileBase pic { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        //[DataType(DataType.Time)]
        public DateTime ATime { get; set; }
        
    }
}