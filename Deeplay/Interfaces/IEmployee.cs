using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IEmployee
    {
        Employee Add(Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
        Employee Get(int id);
    }
}
