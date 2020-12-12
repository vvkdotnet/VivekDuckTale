using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VivekDuckTale.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public string Marks { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}