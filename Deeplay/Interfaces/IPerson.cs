using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IPerson
    {
        Person Add(Person person);
        bool Update(Person person);
        bool Delete(int id);
        Person Get(int id);
    }
}
