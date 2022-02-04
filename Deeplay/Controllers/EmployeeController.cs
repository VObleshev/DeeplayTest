using DataLayer.Context;
using DataLayer.Models;
using DataLayer.ModelsDTO;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using DataLayer.Communication;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class EmployeeController
    {
        private readonly StaffAccountingContext _dbContext;
        private readonly IPerson _personService;
        private readonly IEmployee _employeeService;

        public EmployeeController(StaffAccountingContext dbContext, IPerson personService
                                , IEmployee employeeService)
        {
            _dbContext = dbContext;
            _personService = personService;
            _employeeService = employeeService;
        }

        public IResponsible Get(int id)
        {
            var employee = _employeeService.Get(id);
            if (employee == null)
                return new Response("Пользователя с таким Id не найдено", false, new ArgumentNullException("Employee"));

            var person = _personService.Get(employee.PersonId);
            if (person == null)
                return new Response("Пользователя с таким Id не найдено", false, new ArgumentNullException("Employee.Person"));

            var result = new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                PhoneNumber = person.PhoneNumber,
                DepartmentId = employee.DepartmentId,
                PositionId = employee.PositionId,
                Salary = employee.Salary,
                EmploymentDate = employee.EmploymentDate,
                DismissalDate = employee.DismissalDate
            };

            return new Response("Данные о пользователе успешно получены", true, result);
        }

        public IResponsible Create(EmployeeDTO employee)
        {            
            var person = new Person
            {
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                PhoneNumber = employee.PhoneNumber
            };
                        
            Person newPerson;
            try
            {
                newPerson = _personService.Add(person);
            }
            catch (Exception ex)
            {
                return new Response("При создании сотрудника произошла ошибка", false, ex);
            }

            if (newPerson == null)
            {
                return new Response("При создании сотрудника произошла ошибка", false, new ArgumentNullException("CreatePerson"));
            }

            var newEmployee = new Employee
            {
                PersonId = newPerson.Id,
                DepartmentId = employee.DepartmentId,
                PositionId = employee.PositionId,
                Salary = employee.Salary,
                EmploymentDate = employee.EmploymentDate,
                DismissalDate = employee.DismissalDate
            };

            try
            {
                _employeeService.Add(newEmployee);
            }
            catch (Exception ex)
            {
                return new Response("При создании сотрудника произошла ошибка", false, ex);
            }

            return new Response("Сотрудник успешно создан", true);
        }

        public IResponsible Update(EmployeeDTO employeeDTO)
        {
            var employee = _employeeService.Get(employeeDTO.Id);
            if(employee == null)
                return new Response("Сотрудника не существует", false, new ArgumentNullException("Employee"));

            var person = new Person
            {
                Id = employee.PersonId,
                FirstName = employeeDTO.FirstName,
                MiddleName = employeeDTO.MiddleName,
                LastName = employeeDTO.LastName,
                BirthDate = employeeDTO.BirthDate,
                PhoneNumber = employeeDTO.PhoneNumber
            };

            try
            {
                 _personService.Update(person);             
            }
            catch (Exception ex)
            {
                return new Response("При обновлении данных о сотруднике произошла ошибка", false, ex);
            }

            var newEmployee = new Employee
            {
                Id = employeeDTO.Id,
                DepartmentId = employeeDTO.DepartmentId,
                PositionId = employeeDTO.PositionId,
                Salary = employeeDTO.Salary,
                EmploymentDate = employeeDTO.EmploymentDate,
                DismissalDate = employeeDTO.DismissalDate
            };

            try
            {
                _employeeService.Update(newEmployee);
            }
            catch (Exception ex)
            {
                return new Response("При обновлении данных о сотруднике произошла ошибка", false, ex);
            }

            return new Response("Данные о сотрудники успешно обновлены!", true);
        }

        public IResponsible Delete(int id)
        {
            try
            {
                var employee = _employeeService.Get(id);
                if (employee == null)
                    return new Response("Сотрудника не существует", false, new ArgumentNullException("Employee"));

                _personService.Delete(employee.PersonId);
                _employeeService.Delete(id);
            }
            catch (Exception ex)
            {
                return new Response("При удалении сотрудника произошла ошибка", false, ex);
            }

            return new Response("Сотрудник успешно удален", true);
        }
    }
}
