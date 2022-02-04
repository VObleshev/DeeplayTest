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
    public class PersonService : IPerson
    {
        private readonly StaffAccountingContext _dbContext;

        public PersonService(StaffAccountingContext  dbContext)
        {
            _dbContext = dbContext;
        }

        public Person Add(Person person)
        {
            Person newPerson;
            try
            {
                newPerson = _dbContext.Persons.Add(person).Entity;
                _dbContext.SaveChanges();
            }
            catch
            {
                return null;
            }

            return newPerson;
        }

        public bool Update(Person person)
        {
            var updPerson = _dbContext.Persons.FirstOrDefault(p => p.Id == person.Id);
            if(updPerson == null)
                return false;

            updPerson.FirstName = person.FirstName;
            updPerson.MiddleName = person.MiddleName;
            updPerson.LastName = person.LastName;
            updPerson.BirthDate = person.BirthDate;
            updPerson.PhoneNumber = person.PhoneNumber;            

            try
            {
                _dbContext.Persons.Update(updPerson);
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
            var person = _dbContext.Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
                throw new ArgumentNullException("Person");

            try
            {
                _dbContext.Persons.Remove(person);
                _dbContext.SaveChanges();
            }
            catch            
            {
                return false;
            }

            return true;
        }

        public Person Get(int id)
        {
            var person = _dbContext.Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
                return null;

            return person;
        }
    }
}
