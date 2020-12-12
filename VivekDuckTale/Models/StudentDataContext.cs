using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VivekDuckTale.Models
{
    public class StudentDataContext :DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subject { get; set; }
    }
}