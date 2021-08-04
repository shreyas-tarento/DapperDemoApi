using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemoApi.Models
{
    public class Employee
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [Required]
        public String Department { get; set; }
    }
}
