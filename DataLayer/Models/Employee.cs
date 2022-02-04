using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime DismissalDate { get; set; }
    }
}
