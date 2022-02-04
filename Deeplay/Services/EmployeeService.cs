using DataLayer.Context;
using DataLayer.Models;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly StaffAccountingContext _dbContext;

        public EmployeeService(StaffAccountingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Employee Add(Employee employee)
        {
            Employee newEmployee;
            try
            {
                newEmployee = _dbContext.Employees.Add(employee).Entity;
                _dbContext.SaveChanges();
            }
            catch
            {
                return null;
            }

            return newEmployee;
        }

        public bool Update(Employee employee)
        {
            var updEmploye = _dbContext.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (updEmploye == null)
                return false;

            updEmploye.Salary = employee.Salary;
            updEmploye.DepartmentId = employee.DepartmentId;
            updEmploye.PositionId = employee.PositionId;
            updEmploye.EmploymentDate = employee.EmploymentDate;
            updEmploye.DismissalDate = employee.DismissalDate;

            try
            {
                _dbContext.Employees.Update(updEmploye);
                _dbContext.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                throw new ArgumentNullException("Employee");

            try
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Employee Get(int id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if(employee == null)
                return null;

            return employee;
        }
    }
}
