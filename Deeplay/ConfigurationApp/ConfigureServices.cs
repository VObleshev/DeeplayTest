using DataLayer.Context;
using Microsoft.Extensions.Configuration;
using Server.Interfaces;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ConfigurationApp
{
    public class ConfigureServices
    {
        public void Build()
        {
            DIContainer.Register<StaffAccountingContext, StaffAccountingContext>();
            DIContainer.Register<IPerson, PersonService>();
            DIContainer.Register<IEmployee, EmployeeService>();
            DIContainer.Register<IUser, UserService>();
        }
    }
}
