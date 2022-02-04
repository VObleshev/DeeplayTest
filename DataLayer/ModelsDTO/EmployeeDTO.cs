using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ModelsDTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public long PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
        public decimal Salary { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime DismissalDate { get; set; }
    }
}
