using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IUser
    {
        User Get(string login);
    }
}
