using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VivekDuckTale.Models
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        public string SubjectName { get; set; }
    }
}